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
    public class QueueCreateParameter : BaseParameterBuilder<IServiceBrokerConfig>, IQueueCreateParameter
    {
        public QueueCreateParameter()
        {
        }
        
        protected override IList<SqlParameter> GetCommandParameters(IServiceBrokerConfig configParameters)
        {
            return new List<SqlParameter>()
            {
                new SqlParameter("QueueName", configParameters.QueueName),
                new SqlParameter("ActivationProcedureName", configParameters.ProcedureName)
            };
        }
        protected override bool IsParameterValid(IServiceBrokerConfig configParameters)
        {
            return !String.IsNullOrEmpty(configParameters.QueueName)
                && !String.IsNullOrEmpty(configParameters.ProcedureName);
        }
    }
}
