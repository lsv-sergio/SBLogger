using Services.Enums;
using Services.Interfaces;
using System;

namespace Services.Classes
{
    public class ServiceBrokerConfig : IServiceBrokerConfig
    {
        public PrimaryColumnTypeEnums PrimaryColumnType { get; set; }
        public string ContractName { get; set; }
        public string MessageType { get; set; }
        public string QueueName { get; set; }
        public string ServiceName { get; set; }
        public string ProcedureName { get; set; }
        public string LogTableName { get; set; }
        public string ErrorLogTableName { get; set; }
    }
}
