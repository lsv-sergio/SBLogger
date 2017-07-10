using Services.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IServiceBrokerParameters: IServiceBrokerConfig
    {
        string OldContractName { get; set; }
        string OldMessageType { get; set; }
        string OldQueueName { get; set; }
        string OldServiceName { get; set; }
        string OldProcedureName { get; set; }
        string OldLogTableName { get; set; }
        string OldErrorLogTableName { get; set; }
        CommandOperationTypeEnums ContractOperation { get; set; }
        CommandOperationTypeEnums MessageTypeCommandOperation { get; set; }
        CommandOperationTypeEnums QueueCommandOperation { get; set; }
        CommandOperationTypeEnums ServiceCommandOperation { get; set; }
        CommandOperationTypeEnums ProcedureCommandOperation { get; set; }
        CommandOperationTypeEnums LogTableCommandOperation { get; set; }
        CommandOperationTypeEnums ErrorLogTableCommandOperation { get; set; }
        bool NeedDropContract { get; }
        bool NeedDropMessageType { get; }
        bool NeedDropQueue { get; }
        bool NeedDropService { get; }
        bool NeedDropProcedure { get; }
        bool NeedDropLogTable { get; }
        bool NeedDropErrorLogTable { get; }
        IServiceBrokerConfig GetServiceBrokerConfig();
        IServiceBrokerConfig GetOldServiceBrokerConfig();
    }
}
