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
    public class TriggerDropParameter : BaseParameterBuilder<ITriggerConfigParameters>, ITriggerDropParameter
    {
        public TriggerDropParameter()
            
        {
        }
        protected override IList<SqlParameter> GetCommandParameters(ITriggerConfigParameters configParameters)
        {
            return new List<SqlParameter>()
            {
                new SqlParameter("TriggerName", configParameters.GetTriggerName())
            };
        }
        protected override bool IsParameterValid(ITriggerConfigParameters configParameters)
        {
            return configParameters.IsValid;
        }
    }
}
