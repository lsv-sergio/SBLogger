using System.Data.SqlClient;

namespace Services.Interfaces
{
    public interface IBaseSqlCommand
    {
        void Execute(SqlConnection conn, SqlTransaction transaction, IBaseParameter parameters);
        void Execute(SqlConnection conn, SqlTransaction transaction);
    }
}
