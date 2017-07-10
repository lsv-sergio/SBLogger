using Services.Interfaces;
using Services.Interfaces.Builders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Services.Classes.Commands
{
    public abstract class SpSqlCommand : BaseSqlCommand
    {
        ISqlParameterBuilder _parameterBuilder;
        public SpSqlCommand(string commandText, ISqlParameterBuilder parameterBuilder)
            : base(commandText, CommandType.StoredProcedure)
        {
            _parameterBuilder = parameterBuilder;
        }
        protected override IList<SqlParameter> GetParameters(IBaseParameter parameters)
        {
            if (_parameterBuilder == null)
            {
                return new List<SqlParameter>();
            }
            return _parameterBuilder.GetParameters(parameters);
        }

    }
}
