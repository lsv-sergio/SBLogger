using Services.Interfaces.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Services.Interfaces;
using Services.Interfaces.Builders.Parameters;

namespace Services.Classes.Builders.Parameter
{
    public class QueueServiceDropParameter : BaseParameterBuilder<IServiceBrokerConfig>, IQueueServiceDropParameter
    {
        public QueueServiceDropParameter() 
        {
        }
        protected override IList<SqlParameter> GetCommandParameters(IServiceBrokerConfig configParameters)
        {
            return new List<SqlParameter>()
            {
                new SqlParameter("QueueServiceName", configParameters.ServiceName)
            };
        }
        protected override bool IsParameterValid(IServiceBrokerConfig configParameters)
        {
            return !String.IsNullOrEmpty(configParameters.ServiceName);
        }
    }
}
