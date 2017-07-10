using Services.Interfaces;
using Services.Interfaces.Builders;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Services.Classes.Builders.Parameter
{
    public abstract class BaseParameterBuilder<T> : ISqlParameterBuilder<T>
    {
        protected abstract bool IsParameterValid(T configParameters);
        protected abstract IList<SqlParameter> GetCommandParameters(T configParameters);
        public IList<SqlParameter> GetParameters(T configParameters)
        {
            if (configParameters == null || !IsParameterValid(configParameters))
            {
                throw new InvalidOperationException("Invalid parameters");
            }
            return GetCommandParameters(configParameters);
        }
    }
}
