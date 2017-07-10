using Services.Classes;
using Services.Classes.Builders;
using Services.Classes.Builders.Parameter;
using Services.Classes.Builders.SqlObjectBuilders;
using Services.Classes.Commands.CreateCommands;
using Services.Classes.Commands.DropCommands;
using Services.Classes.Commands.SBConfigCommands;
using Services.Interfaces;
using Services.Interfaces.Builders;
using Services.Interfaces.ServiceBrokerConfigInterfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Services
{
    public class LoggerConfigurator
    {
        public LoggerConfigurator()
        {
        }

        public void Install(SqlConnection conn)
        {

            ExecuteCommands(conn, new ConfigObjectBuilder().BuildCreateCommands());
        }
        public void UnInstall(SqlConnection conn)
        {
            ExecuteCommands(conn, new ConfigObjectBuilder().BuildDropCommands(), null);
        }

        public void InstallObjects(SqlConnection conn, IServiceBrokerParameters parameters)
        {
            UpdateObject<SetupObjectBuilder>(conn, parameters, true);
        }
        public void UnInstallObjects(SqlConnection conn, IServiceBrokerParameters parameters)
        {
            var setupBuilder = new SetupObjectBuilder();
            ExecuteCommands(conn, setupBuilder.BuildDropCommands(), parameters.GetServiceBrokerConfig());
        }
        public void UpdateMessageType(SqlConnection conn, IServiceBrokerParameters parameters)
        {
            UpdateObject<MessageTypeUpdateBuilder>(conn, parameters, parameters.NeedDropContract);
        }
        public void UpdateContract(SqlConnection conn, IServiceBrokerParameters parameters)
        {
            UpdateObject<ContractUpdateBuilder>(conn, parameters, parameters.NeedDropContract);
        }
        public void UpdateErrorLogTable(SqlConnection conn, IServiceBrokerParameters parameters)
        {
            UpdateObject<ErrorLogTableUpdateBuilder>(conn, parameters, parameters.NeedDropErrorLogTable);
        }
        public void UpdateLogTable(SqlConnection conn, IServiceBrokerParameters parameters)
        {
            UpdateObject<LogTableUpdateBuilder>(conn, parameters, parameters.NeedDropLogTable);
        }
        public void UpdateQueue(SqlConnection conn, IServiceBrokerParameters parameters)
        {
            UpdateObject<QueueUpdateBuilder>(conn, parameters, parameters.NeedDropQueue);
        }
        public void UpdateQueueService(SqlConnection conn, IServiceBrokerParameters parameters)
        {
            UpdateObject<QueueServiceUpdateBuilder>(conn, parameters, parameters.NeedDropService);
        }
        public void UpdateActivationProcedure(SqlConnection conn, IServiceBrokerParameters parameters)
        {
            UpdateObject<ActivationProcedureUpdateBuilder>(conn, parameters, parameters.NeedDropProcedure);
        }

        private void ExecuteCommands(SqlConnection conn, IList<IBaseSqlCommand> commands, IServiceBrokerConfig parameters)
        {
            if (!CheckConnection(conn) || commands.Count == 0)
            {
                return;
            }
            var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            try
            {
                foreach (var command in commands)
                {
                    command.Execute(conn, tran, parameters);
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                tran.Rollback();
            }
        }
        private void ExecuteCommands(SqlConnection conn, IList<IBaseSqlCommand> commands)
        {
            ExecuteCommands(conn, commands, null);
        }
        private void UpdateObject<T>(SqlConnection conn, IServiceBrokerParameters parameters, bool needDropOldObject) where T: ISqlObjectBuilder
        {
            if (!CheckConnection(conn) || parameters == null)
            {
                return;
            }
            var commandBuilder = Activator.CreateInstance<T>();
            if (commandBuilder == null)
            {
                return;
            }
            ExecuteCommands(conn, commandBuilder.BuildCreateCommands(), parameters.GetServiceBrokerConfig());
            if (needDropOldObject)
            {
                ExecuteCommands(conn, commandBuilder.BuildCreateCommands(), parameters.GetOldServiceBrokerConfig());
            }
        }
        private bool CheckConnection(SqlConnection conn)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                // TODO throw Exception
                return false;
            }
            return true;
        }
    }
}
