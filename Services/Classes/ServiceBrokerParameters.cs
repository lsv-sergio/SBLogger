using Services.Enums;
using Services.Interfaces;
using System;

namespace Services.Classes
{
    public class ServiceBrokerParameters : IServiceBrokerParameters
    {
        public ServiceBrokerParameters()
        {
        }
        public PrimaryColumnTypeEnums PrimaryColumnType { get; set; }
        public string ContractName { get; set; }
        public string OldContractName { get; set; }
        public string MessageType { get; set; }
        public string OldMessageType { get; set; }
        public string QueueName { get; set; }
        public string OldQueueName { get; set; }
        public string ServiceName { get; set; }
        public string OldServiceName { get; set; }
        public string ProcedureName { get; set; }
        public string OldProcedureName { get; set; }
        public string LogTableName { get; set; }
        public string OldLogTableName { get; set; }
        public string ErrorLogTableName { get; set; }
        public string OldErrorLogTableName { get; set; }
        public CommandOperationTypeEnums ContractOperation { get; set; }
        public CommandOperationTypeEnums MessageTypeCommandOperation { get; set; }
        public CommandOperationTypeEnums QueueCommandOperation { get; set; }
        public CommandOperationTypeEnums ServiceCommandOperation { get; set; }
        public CommandOperationTypeEnums ProcedureCommandOperation { get; set; }
        public CommandOperationTypeEnums LogTableCommandOperation { get; set; }
        public CommandOperationTypeEnums ErrorLogTableCommandOperation { get; set; }
        public bool NeedDropContract
        {
            get { return NeedDropObject(ContractName, OldContractName, ContractOperation); }
        }
        public bool NeedDropMessageType
        {
            get { return NeedDropObject(ContractName, OldContractName, ContractOperation); }
        }
        public bool NeedDropQueue
        {
            get { return NeedDropObject(ContractName, OldContractName, ContractOperation); }
        }
        public bool NeedDropService
        {
            get { return NeedDropObject(ContractName, OldContractName, ContractOperation); }
        }
        public bool NeedDropProcedure
        {
            get { return NeedDropObject(ContractName, OldContractName, ContractOperation); }
        }
        public bool NeedDropLogTable
        {
            get { return NeedDropObject(ContractName, OldContractName, ContractOperation); }
        }
        public bool NeedDropErrorLogTable
        {
            get { return NeedDropObject(ContractName, OldContractName, ContractOperation); }
        }
        public bool IsValid
        {
            get
            {
                return !String.IsNullOrEmpty(ContractName)
                    && !String.IsNullOrEmpty(MessageType)
                    && !String.IsNullOrEmpty(QueueName)
                    && !String.IsNullOrEmpty(ServiceName)
                    && !String.IsNullOrEmpty(ProcedureName)
                    && !String.IsNullOrEmpty(LogTableName)
                    && !String.IsNullOrEmpty(ErrorLogTableName);
            }
        }

        public IServiceBrokerConfig GetServiceBrokerConfig()
        {
            return new ServiceBrokerConfig()
            {
                ContractName = ContractName,
                MessageType = MessageType,
                QueueName = QueueName,
                ServiceName = ServiceName,
                ProcedureName = ProcedureName,
                LogTableName = LogTableName,
                ErrorLogTableName = ErrorLogTableName,
                PrimaryColumnType = PrimaryColumnType
            };
    }
        public IServiceBrokerConfig GetOldServiceBrokerConfig()
        {
            return new ServiceBrokerConfig()
            {
                ContractName = OldContractName,
                MessageType = OldMessageType,
                QueueName = OldQueueName,
                ServiceName = OldServiceName,
                ProcedureName = OldProcedureName,
                LogTableName = OldLogTableName,
                ErrorLogTableName = OldErrorLogTableName
            };
    }

        private bool NeedDropObject(string objectName, string oldObjectName, CommandOperationTypeEnums commandOperation)
        {
            return !objectName.Equals(oldObjectName) 
                && !String.IsNullOrEmpty(oldObjectName)
                && commandOperation > 0;
        }
    }
}
