using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Services.Interfaces;
using Services.Enums;
using Services.Interfaces.Builders.Parameters;

namespace Services.Classes.Builders.Parameter
{
    public class ActivationProcedureCreateParameter : BaseParameterBuilder<IServiceBrokerConfig>, IActivationProcedureCreateParameter
    {
        public ActivationProcedureCreateParameter()
        {
        }
        
        protected override IList<SqlParameter> GetCommandParameters(IServiceBrokerConfig configParameters)
        {
            return new List<SqlParameter>()
            {
                new SqlParameter("ActivationProcedureName", configParameters.ProcedureName),
                new SqlParameter("QueueName", configParameters.QueueName),
                new SqlParameter("MessageTypeName", configParameters.MessageType),
                new SqlParameter("PrimaryColumnType", Enum.GetName(typeof(PrimaryColumnTypeEnums), configParameters.PrimaryColumnType)),
                new SqlParameter("LogTableName", configParameters.LogTableName),
                new SqlParameter("ErrorWriteLogTableName", configParameters.ErrorLogTableName)
            };
        }

        protected override bool IsParameterValid(IServiceBrokerConfig configParameters)
        {
            return !String.IsNullOrEmpty(configParameters.ProcedureName)
                && !String.IsNullOrEmpty(configParameters.QueueName)
                && !String.IsNullOrEmpty(configParameters.MessageType)
                && !String.IsNullOrEmpty(configParameters.LogTableName)
                && !String.IsNullOrEmpty(configParameters.ErrorLogTableName)
                && configParameters.PrimaryColumnType > 0;
        }
    }
}
