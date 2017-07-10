	DECLARE @BrokerEnabled BIT; 
	SELECT @BrokerEnabled = is_broker_enabled from sys.databases db where db.database_id = DB_ID()
	if (@BrokerEnabled = 0)
	BEGIN
		DECLARE @DynamicSql NVARCHAR(MAX) = N'ALTER DATABASE [' +  DB_NAME() + '] SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE'
		EXEC sp_executesql @DynamicSql; 
		SET @DynamicSql = N'ALTER DATABASE [' +  DB_NAME() + '] SET NEW_BROKER WITH ROLLBACK IMMEDIATE'
		EXEC sp_executesql @DynamicSql; 
	END;
