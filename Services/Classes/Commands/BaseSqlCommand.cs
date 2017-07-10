using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Services.Classes.Commands
{
    public abstract class BaseSqlCommand : IBaseSqlCommand
    {
        private string _commandText;
        private CommandType _commandType;
        public BaseSqlCommand(string commandText, CommandType commandType)
        {
            this._commandText = commandText;
            _commandType = commandType;
        }
        public virtual void Execute(SqlConnection conn, SqlTransaction transaction, IBaseParameter parameters)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                throw new System.Exception("Connection is not open");
            }
            var sqlCommand = conn.CreateCommand();
            sqlCommand.CommandText = _commandText;
            sqlCommand.CommandType = _commandType;
            AddParameters(sqlCommand, parameters);
            sqlCommand.Transaction = transaction;
            sqlCommand.ExecuteNonQuery();
        }

        private void AddParameters(SqlCommand sqlCommand, IBaseParameter sbParameters)
        {
            var sqlParameters = GetParameters(sbParameters);
            if (sqlParameters == null)
            {
                return;
            }
            foreach(SqlParameter sqlParam in sqlParameters)
            {
                sqlCommand.Parameters.Add(sqlParam);
            }
        }

        protected abstract IList<SqlParameter> GetParameters(IBaseParameter parameters);

        public void Execute(SqlConnection conn, SqlTransaction transaction)
        {
            Execute(conn, transaction, null);
        }
    }
}
