using Services.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IServiceBrokerConfig: IBaseParameter
    {
        PrimaryColumnTypeEnums PrimaryColumnType { get; set; }
        string ContractName { get; set; }
        string MessageType { get; set; }
        string QueueName { get; set; }
        string ServiceName { get; set; }
        string ProcedureName { get; set; }
        string LogTableName { get; set; }
        string ErrorLogTableName { get; set; }
    }
}
