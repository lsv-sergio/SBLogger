using Services.Interfaces.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;
using Services.Interfaces;
using Services.Interfaces.Builders.Parameters;

namespace Services.Classes.Builders.Parameter
{
    public class TriggerCreateParameter : BaseParameterBuilder<ITriggerConfigParameters>, ITriggerCreateParameter
    {
        public TriggerCreateParameter()
            
        {
        }
        protected override IList<SqlParameter> GetCommandParameters(ITriggerConfigParameters configParameters)
        {
            return new List<SqlParameter>()
            {
                new SqlParameter("TriggerName", configParameters.GetTriggerName()),
                new SqlParameter("TableName", configParameters.EntityForLog.TableName),
                new SqlParameter("TriggerEvents", configParameters.GetTriggerActions()),
                new SqlParameter("ServiceName", configParameters.ServiceName),
                new SqlParameter("LogFields", configParameters.EntityForLog.LogFields),
                new SqlParameter("IdField", configParameters.EntityForLog.IdField),
                new SqlParameter("ModifiedByIdField", configParameters.EntityForLog.ModifiedByIdField),
                new SqlParameter("ModifiedOnField", configParameters.EntityForLog.ModifiedOnField),
                new SqlParameter("ContractName", configParameters.ContractName),
                new SqlParameter("MessageTypeName", configParameters.MessageType)
            };
        }
        protected override bool IsParameterValid(ITriggerConfigParameters parameters)
        {
            return parameters.IsValid; 
                }
    }
}
