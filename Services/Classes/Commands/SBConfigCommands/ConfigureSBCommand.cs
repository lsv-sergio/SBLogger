using Services.Interfaces.ServiceBrokerConfigInterfaces;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using Services.Interfaces;

namespace Services.Classes.Commands.SBConfigCommands
{
    public class ConfigureSBCommand: BaseSqlCommand, IConfigureSBCommand
    {
        public ConfigureSBCommand(string commandText): base(commandText, System.Data.CommandType.Text)
        {
        }
        protected override IList<SqlParameter> GetParameters(IServiceBrokerConfig parameters)
        {
            return new List<SqlParameter>();
        }
    }
}
