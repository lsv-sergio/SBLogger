DECLARE @DynamicSql NVARCHAR(MAX) = '
	CREATE PROCEDURE sp_Drop_ActivationProcedure
		@ActivationProcedureName AS NVARCHAR(MAX)
	AS
	BEGIN	
		SET NOCOUNT ON;
		IF (@ActivationProcedureName = '''') 
		BEGIN
			RETURN;
		END;
		DECLARE @DynamicSql as NVARCHAR(MAX) = N''
		IF OBJECT_ID(''''#ActivationProcedureName#'''', ''''P'''') IS NOT NULL 
		BEGIN
			DROP PROCEDURE [#ActivationProcedureName#];
		END;''
		EXECUTE sp_executesql @DynamicSql
	END;'
EXEC sp_executesql @DynamicSql

SET  @DynamicSql = '
	CREATE PROCEDURE sp_CreateUpdate_ActivationProcedure
		@ActivationProcedureName AS NVARCHAR(MAX),
		@QueueName AS NVARCHAR(MAX),
		@MessageTypeName AS NVARCHAR(MAX),
		@PrimaryColumnType AS NVARCHAR(MAX),
		@LogTableName AS NVARCHAR(MAX),
		@ErrorWriteLogTableName AS NVARCHAR(MAX)
	AS
	BEGIN
		SET NOCOUNT ON;

		IF (@ActivationProcedureName = '''' 
		OR @QueueName = ''''
		OR @MessageTypeName = ''''
		OR @LogTableName = ''''
		OR @ErrorWriteLogTableName = '''')
		BEGIN
			-- RAISEERROR ??
			RETURN;
		END;
		DECLARE @DynamicSql NVARCHAR(MAX);
		IF (OBJECT_ID('''' + @ActivationProcedureName + '''', ''P'') IS NULL)
			BEGIN
				SET @DynamicSql = N''CREATE PROCEDURE '' + @ActivationProcedureName;
			END
		ELSE 
			BEGIN
				SET @DynamicSql = N''ALTER PROCEDURE '' + @ActivationProcedureName;
			END
		SET @DynamicSql = @DynamicSql + N''
			WITH EXECUTE AS OWNER
		AS
		BEGIN
			SET NOCOUNT ON;
			DECLARE @Ch UNIQUEIDENTIFIER;
			DECLARE @MsgTypeName NVARCHAR(256);
			DECLARE @MsgBody XML;
			DECLARE @ReplyMsg XML;
			DECLARE @Error INT;
			DECLARE @ErrorMessage NVARCHAR(4000);
			WHILE 1 = 1
				BEGIN
					;RECEIVE  TOP (1)
						 @Ch = conversation_handle,
						 @MsgTypeName = message_type_name,
						 @MsgBody = CAST(message_body AS XML)
					FROM ['' + @QueueName + ''];
					IF @@ROWCOUNT = 0
						BREAK;
					IF ( @MsgTypeName = ''''http://schemas.microsoft.com/SQL/ServiceBroker/EndDialog'''')
					BEGIN
						END CONVERSATION @Ch;
						CONTINUE;
					END;
					IF ( @MsgTypeName != '''' + @MessageTypeName + '''')
					BEGIN
						END CONVERSATION @Ch;
						CONTINUE;
					END;
					IF @MsgBody IS NOT NULL
						BEGIN TRY
							DECLARE @idoc INT;
							EXEC sp_xml_preparedocument @idoc OUT, @MsgBody;
							INSERT  INTO ['' + @LogTableName + ''] ( ObjectName , ObjectId , RecordId , ModifiedOn , ModifiedById , ActionType , CurrentData , OldData)
							SELECT  
								parsedData.ObjectName AS ObjectName ,
								OBJECT_ID(parsedData.ObjectName) ,
								parsedData.RecordId ,
								parsedData.ModifiedOn ,
								parsedData.ModifiedById ,
								parsedData.ActionType ,
								CAST(parsedData.currentData AS NVARCHAR(MAX)),
								CAST(parsedData.oldData AS NVARCHAR(MAX))
							FROM	OPENXML(@idoc, ''''/root/Entity'''',1)
							WITH (
								ModifiedOn [DATETIME] ''''@ModifiedOn'''',
								ModifiedById ['' + @PrimaryColumnType + ''] ''''@ModifiedById'''',
								RecordId ['' + @PrimaryColumnType + ''] ''''@RecordId'''',
								ActionType [INT] ''''@ActionType'''',
								ObjectName [NVARCHAR](500) ''''@ObjectName'''',
								oldData [XML] ''''oldData'''',
								currentData [XML] ''''currentData''''
							) AS parsedData
							 INNER JOIN sys.tables st ON st.name = parsedData.ObjectName
							EXEC sp_xml_removedocument @idoc;
						END TRY
						BEGIN CATCH 
							SELECT  
								@Error = ERROR_NUMBER() ,
								@ErrorMessage = ERROR_MESSAGE();
							IF ( XACT_STATE() ) = 1
							BEGIN
								SELECT  
									@Error = ERROR_NUMBER() ,
									@ErrorMessage = ERROR_MESSAGE();
								END CONVERSATION @Ch WITH ERROR = @Error DESCRIPTION = @ErrorMessage;
								COMMIT;
							END;
							IF ( XACT_STATE() ) = -1
							BEGIN
								SELECT  
									@Error = ERROR_NUMBER() ,
									@ErrorMessage = ERROR_MESSAGE();
								BEGIN TRANSACTION;
									RECEIVE TOP(1)
										@Ch = conversation_handle,
										@MsgTypeName = message_type_name,
										 @MsgBody = CAST(message_body AS XML)
									FROM dbo.ReceiverQueue
									END CONVERSATION @Ch WITH ERROR = @Error DESCRIPTION = @ErrorMessage; 
								COMMIT;
							END; 
							INSERT  INTO ['' + @ErrorWriteLogTableName + '']( CreatedOn , ErrorNumber , ErrorText , MessageBody )
							VALUES  ( GETUTCDATE() , @Error , @ErrorMessage , @MsgBody )
						END  CATCH;
				END;
		END;'';
		EXECUTE sp_executesql @DynamicSql
	END;'
EXEC sp_executesql @DynamicSql

SET @DynamicSql = '
	CREATE PROCEDURE sp_Drop_Contract
		@ContractName NVARCHAR(MAX)
	AS
	BEGIN
		SET NOCOUNT ON;

		IF(@ContractName = '''') 
		BEGIN
			RETURN;
		END;

		DECLARE @DynamicSqlTemplate as NVARCHAR(MAX) = N''
		IF EXISTS(SELECT NULL FROM sys.service_contracts AS sc  WHERE sc.name = ''''#ContractName#'''') BEGIN
			DROP CONTRACT [#ContractName#];
		END;''

		DECLARE @DynamicSql NVARCHAR(MAX);
		SET @DynamicSql = REPLACE(@DynamicSqlTemplate, ''#ContractName#'', @ContractName)
		EXECUTE sp_executesql @DynamicSql

	END';
EXEC sp_executesql @DynamicSql

SET @DynamicSql = '
	CREATE PROCEDURE sp_Create_Contract
		@ContractName NVARCHAR(MAX),
		@MessageTypeName NVARCHAR(MAX)
	AS
	BEGIN
		SET NOCOUNT ON;

		IF (@ContractName = '''' OR @MessageTypeName = '''')
		BEGIN
			--RAISEERROR ??
			RETURN;
		END;

		DECLARE @DynamicSql NVARCHAR(MAX) = N''CREATE CONTRACT ['' + @ContractName + ''] ('' + @MessageTypeName + '' SENT BY ANY);'';
		EXECUTE sp_executesql @DynamicSql

	END;'
EXEC sp_executesql @DynamicSql

SET @DynamicSql = '
	CREATE PROCEDURE sp_Drop_ErrorLogTable
		@ErrorLogTableName NVARCHAR(MAX)
	AS
	BEGIN
		SET NOCOUNT ON;

		IF (@ErrorLogTableName = '''')
		BEGIN
			RETURN;
		END;

		DECLARE @DynamicSql as NVARCHAR(MAX) = N''
		IF OBJECT_ID(''''#ErrorLogTableName#'''') IS NOT NULL 
		BEGIN
			DROP TABLE [#ErrorLogTableName#];
		END;''

		SET @DynamicSql = REPLACE(@DynamicSql, ''#ErrorLogTableName#'', @ErrorLogTableName)
		EXECUTE sp_executesql @DynamicSql

	END';
EXEC sp_executesql @DynamicSql

SET @DynamicSql = '
	CREATE PROCEDURE sp_Create_ErrorLogTable
		@ErrorLogTableName NVARCHAR(MAX)
	AS
	BEGIN
		SET NOCOUNT ON;

		IF (@ErrorLogTableName = '''')
		BEGIN
			-- RAISEERROR ??
			RETURN;
		END;

		DECLARE @DynamicSql NVARCHAR(MAX) = N''
		CREATE TABLE [#ErrorLogTableName#] 
		(
			[Id] [INT] IDENTITY(1,1) NOT NULL,  
			[CreatedOn] [DATETIME] NOT NULL,
			[ErrorNumber] [INT] NOT NULL,
			[ErrorText ] [NVARCHAR](MAX) NOT NULL,
			[MessageBody] [XML]
		)'';

		SET @DynamicSql = REPLACE(@DynamicSql, ''#ErrorLogTableName#'', @ErrorLogTableName)
		EXECUTE sp_executesql @DynamicSql

	END;'
EXEC sp_executesql @DynamicSql

SET @DynamicSql = '
	CREATE PROCEDURE sp_Drop_LogTable
		@LogTableName NVARCHAR(MAX)
	AS
	BEGIN
		SET NOCOUNT ON;

		IF (@LogTableName = '''')
		BEGIN
			RETURN;
		END;

		DECLARE @DynamicSql as NVARCHAR(MAX) = N''
		IF OBJECT_ID(''''#LogTableName#'''') IS NOT NULL 
		BEGIN
			DROP TABLE [#LogTableName#];
		END;''
	
		SET @DynamicSql = REPLACE(@DynamicSql, ''#LogTableName#'', @LogTableName)
		EXECUTE sp_executesql @DynamicSql

	END'
EXEC sp_executesql @DynamicSql

SET @DynamicSql = '
	CREATE PROCEDURE sp_Create_LogTable
		@LogTableName NVARCHAR(MAX),
		@PrimaryColumnType NVARCHAR(MAX)
	AS
	BEGIN
		SET NOCOUNT ON;

		IF (@LogTableName = '''' OR @PrimaryColumnType = '''')
		BEGIN
			-- RAISEERROR ??
			RETURN;
		END;

		DECLARE @DynamicSql NVARCHAR(MAX) = N''
		CREATE TABLE ['' + @LogTableName + ''] 
		(
			[Id] [BIGINT] IDENTITY(1,1) NOT NULL,
			[ModifiedOn] [DATETIME] NOT NULL,
			[ModifiedById] ['' + @PrimaryColumnType + ''] NOT NULL,
			[RecordId] ['' + @PrimaryColumnType + ''] NOT NULL,
			[ActionType] [INT] NOT NULL,
			[ObjectId] [INT] NOT NULL,
			[ObjectName] [NVARCHAR](500) NOT NULL,
			[oldData] [XML],
			[currentData] [XML]
		)'';

		EXECUTE sp_executesql @DynamicSql

	END;'
EXEC sp_executesql @DynamicSql

SET @DynamicSql = '
	CREATE PROCEDURE sp_Drop_MessageType
		@MessageTypeName NVARCHAR(MAX)
	AS
	BEGIN
		SET NOCOUNT ON;

		IF (@MessageTypeName = '''')
		BEGIN
			RETURN;
		END;

		DECLARE @DynamicSql as NVARCHAR(MAX) = N''
		IF EXISTS(SELECT NULL FROM sys.service_message_types AS sm  WHERE sm.name = ''''#MessageTypeName#'''') BEGIN
			DROP MESSAGE TYPE [#MessageTypeName#];
		END;''

		SET @DynamicSql = REPLACE(@DynamicSql, ''#MessageTypeName#'', @MessageTypeName)
		EXECUTE sp_executesql @DynamicSql

	END';
EXEC sp_executesql @DynamicSql


SET @DynamicSql = '
	CREATE PROCEDURE sp_Create_MessageType
		@MessageTypeName NVARCHAR(MAX)
	AS
	BEGIN
		SET NOCOUNT ON;

		IF (@MessageTypeName = '''')
		BEGIN
			--RAISEERROR(''sp_Create_MessageType'', 16, 1)
			RETURN;
		END;

		DECLARE @DynamicSql NVARCHAR(MAX) = N''CREATE MESSAGE TYPE [#MessageTypeName#] VALIDATION = NONE;'';

		SET @DynamicSql = REPLACE(@DynamicSql, ''#MessageTypeName#'', @MessageTypeName)
		EXECUTE sp_executesql @DynamicSql

	END;'
EXEC sp_executesql @DynamicSql

SET @DynamicSql = '
	CREATE PROCEDURE sp_Drop_Queue
		@QueueName NVARCHAR(MAX)
	AS
	BEGIN
		SET NOCOUNT ON;

		IF (@QueueName = '''')
		BEGIN
			-- RAISEERROR ??
			RETURN;
		END;
		
		DECLARE @DynamicSql as NVARCHAR(MAX) = N''
		IF EXISTS(SELECT NULL FROM sys.service_queues AS sq  WHERE sq.name = ''''#QueueName#'''') BEGIN
			DROP Queue [#QueueName#];
		END;''

		SET @DynamicSql = REPLACE(@DynamicSql, ''#QueueName#'', @QueueName)
		EXECUTE sp_executesql @DynamicSql

		END';
EXEC sp_executesql @DynamicSql

SET @DynamicSql  = '
	CREATE PROCEDURE sp_CreateUpdate_Queue
		@QueueName NVARCHAR(MAX),
		@ActivationProcedureName NVARCHAR(MAX)
	AS
	BEGIN
		SET NOCOUNT ON;

		IF (@QueueName = '''' OR @ActivationProcedureName = '''')
		BEGIN
			-- RAISEERROR ??
			RETURN;
		END;

		DECLARE @DynamicSql NVARCHAR(MAX);

		IF EXISTS(SELECT NULL FROM sys.service_queues AS sq  WHERE sq.name = '''''''' + @QueueName + '''''''') 
			BEGIN
				SET @DynamicSql = N''ALTER QUEUE ['' + @QueueName + ''] '';
			END
		ELSE
			BEGIN
				SET @DynamicSql = N''CREATE QUEUE ['' + @QueueName + ''] '';
			END
			SET @DynamicSql = @DynamicSql + N'' 
				WITH ACTIVATION (MAX_QUEUE_READERS = 10, PROCEDURE_NAME = '' 
				+ @ActivationProcedureName  + '',  EXECUTE AS OWNER, STATUS=ON)'';

		EXECUTE sp_executesql @DynamicSql
	END;'
EXEC sp_executesql @DynamicSql

SET @DynamicSql = '
	CREATE PROCEDURE sp_Drop_ServiceQueue
		@QueueServiceName NVARCHAR(MAX)
	AS
	BEGIN
		SET NOCOUNT ON;
		
		IF (@QueueServiceName = '''')
		BEGIN
			--RAISEERROR ??
			RETURN;
		END;

		DECLARE @DynamicSql as NVARCHAR(MAX) = N''
		IF EXISTS(SELECT NULL FROM sys.services WHERE name = ''''#QueueServiceName#'''') BEGIN
			DROP SERVICE [#QueueServiceName#];
		END;''

		SET @DynamicSql = REPLACE(@DynamicSql, ''#QueueServiceName#'', @QueueServiceName)
		EXECUTE sp_executesql @DynamicSql
	END';
EXEC sp_executesql @DynamicSql

SET @DynamicSql = '
	CREATE PROCEDURE sp_Create_ServiceQueue
		@QueueServiceName NVARCHAR(MAX),
		@QueueName NVARCHAR(MAX),
		@ContractName NVARCHAR(MAX),
		@MessageTypeName NVARCHAR(MAX)
	AS
	BEGIN
		SET NOCOUNT ON;

		IF (@QueueServiceName = '''' 
			OR @QueueName = ''''
			OR @ContractName = ''''
			OR @MessageTypeName = '''')
		BEGIN
			--RAISEERROR ??
			RETURN;
		END;

		DECLARE @DynamicSql NVARCHAR(MAX) = N''CREATE SERVICE ''+ @QueueServiceName + '' ON QUEUE ['' + @QueueName + ''](['' + @ContractName + '']);'';

		EXECUTE sp_executesql @DynamicSql

	END;'
EXEC sp_executesql @DynamicSql

SET @DynamicSql = '
CREATE PROCEDURE sp_CreateUpdate_Trigger
	@TriggerName NVARCHAR(MAX),
	@TableName NVARCHAR(MAX),
	@TriggerEvents NVARCHAR(MAX),
	@ServiceName NVARCHAR(MAX),
	@LogFields NVARCHAR(MAX),
	@IdField NVARCHAR(MAX),
	@ModifiedByIdField NVARCHAR(MAX),
	@ModifiedOnField NVARCHAR(MAX),
	@ContractName NVARCHAR(MAX),
	@MessageTypeName NVARCHAR(MAX)
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @CurrentDataFields  NVARCHAR(MAX) = N''CurrentData.'' + REPLACE(@LogFields, '', '', '',  CurrentData.'');
	DECLARE @OldDataFields  NVARCHAR(MAX) = REPLACE(@CurrentDataFields, ''CurrentData'', ''OldData'');
	DECLARE @InsFields  NVARCHAR(MAX) = REPLACE(@CurrentDataFields, ''CurrentData'',  ''ins'');
	DECLARE @DelFields  NVARCHAR(MAX) = REPLACE(@CurrentDataFields, ''CurrentData'', ''del'');

	DECLARE @sql NVARCHAR(MAX);
	IF (OBJECT_ID('''' + @TriggerName + '''') IS NULL)
		BEGIN
			SET @sql = N''CREATE TRIGGER '' + @TriggerName;
		END
	ELSE 
		BEGIN
			SET @sql = N''ALTER TRIGGER '' + @TriggerName;
		END
	SET @sql = @sql + N'' ON [dbo].'' + @TableName + ''
			AFTER '' + @TriggerEvents + ''
			AS
			BEGIN
			SET NOCOUNT ON;
			IF NOT EXISTS(SELECT  NULL
				FROM	sys.services
				WHERE   name = '''''' + @ServiceName + '''''')
				BEGIN
					RETURN;
				END;
			IF NOT EXISTS(SELECT '' + @IdField + '', '' + @LogFields + '' FROM Inserted
				EXCEPT
			SELECT '' + @IdField + '', '' + @LogFields + '' FROM Deleted)
			BEGIN
				RETURN;
			END;
			DECLARE @xmlMessage XML;
			SELECT @xmlMessage = 
				(SELECT Entity.'' + @IdField + '' AS RecordId,
				Entity.'' + @ModifiedByIdField + '' AS ModifiedById,
				Entity.'' + @ModifiedOnField + '' AS ModifiedOn,
				 CASE WHEN NOT EXISTS (SELECT NULL FROM Inserted inserted WHERE inserted.'' + @IdField + '' = Entity.'' + @IdField + '' ) THEN 3
				 WHEN NOT EXISTS (SELECT NULL FROM Deleted deleted WHERE deleted.'' + @IdField + '' = Entity.'' + @IdField + '') THEN 1
				 ELSE 2 END AS ActionType,
				'''''' + @TableName + '''''' AS ObjectName,
				OBJECT_ID('''''' + @TableName + '''''') AS ObjectId,
				(SELECT currentData.'' + @IdField + '', '' + @CurrentDataFields + '' FROM  Inserted currentData  WHERE currentData.'' + @IdField + '' = Entity.'' + @IdField + '' FOR XML PATH (''''currentData''''), TYPE, BINARY BASE64),
				(SELECT oldData.'' + @IdField + '', ''  + @OldDataFields + '' FROM Deleted oldData  WHERE oldData.'' + @IdField + '' = Entity.'' + @IdField + '' FOR XML PATH (''''oldData''''), TYPE, BINARY BASE64)
				FROM Inserted Entity 
				WHERE EXISTS (SELECT ins.'' + @IdField + '', '' + @InsFields + '' FROM Inserted ins  WHERE ins.'' + @IdField + '' = Entity.'' + @IdField + ''
					EXCEPT
					SELECT del.'' + @IdField + '', '' + @DelFields + '' FROM  Deleted del  WHERE del.'' + @IdField + '' = Entity.'' + @IdField + '')
				 FOR XML AUTO, TYPE, ROOT);
			IF(@xmlMessage IS NULL)
			BEGIN
				RETURN;
			END;
			DECLARE @ConvHandle UNIQUEIDENTIFIER;
			BEGIN DIALOG @ConvHandle
				FROM SERVICE '' + @ServiceName + '' TO SERVICE
				'''''' + @ServiceName + ''''''
				ON CONTRACT ['' + @ContractName + ''] WITH ENCRYPTION = OFF;
			SEND ON CONVERSATION @ConvHandle MESSAGE TYPE ['' + @MessageTypeName + ''] (@xmlMessage);
			END CONVERSATION @ConvHandle;
		END;'';

		EXEC sp_executesql @sql
	END;'
EXEC sp_executesql @DynamicSql

SET @DynamicSql = '
	CREATE PROCEDURE sp_Drop_Trigger
		@TriggerName NVARCHAR(MAX)
	AS
	BEGIN
		SET NOCOUNT ON;

		IF(@TriggerName = '''') 
		BEGIN
			RETURN;
		END;

		DECLARE @DynamicSql as NVARCHAR(MAX) = N''
		IF OBJECT_ID('''''' + @TriggerName + '''''') IS NOT NULL
		BEGIN
			DROP TRIGGER ['' + @TriggerName + ''];
		END;''

		EXECUTE sp_executesql @DynamicSql

	END';

EXEC sp_executesql @DynamicSql
