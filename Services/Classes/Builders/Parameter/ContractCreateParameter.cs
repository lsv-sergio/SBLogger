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
    public class ContractCreateParameter : BaseParameterBuilder<IServiceBrokerConfig>, IContractCreateParameter
    {
        public ContractCreateParameter()
        {
        }
        protected override IList<SqlParameter> GetCommandParameters(IServiceBrokerConfig configParameters)
        {
            return new List<SqlParameter>()
            {
                new SqlParameter("ContractName", configParameters.ContractName),
               new SqlParameter("MessageTypeName", configParameters.MessageType)
            };
        }
        protected override bool IsParameterValid(IServiceBrokerConfig configParameters)
        {
            return !String.IsNullOrEmpty(configParameters.ContractName) 
                && !String.IsNullOrEmpty(configParameters.MessageType);
        }
    }
}
