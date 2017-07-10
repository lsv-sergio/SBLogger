using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Services.Interfaces.Builders
{
    public interface ISqlParameterBuilder<T>
    {
        IList<SqlParameter> GetParameters(T parameters);
    }
}
