using Services.Classes.Builders.Parameter;
using Services.Classes.Commands.CreateCommands;
using Services.Classes.Commands.DropCommands;
using Services.Interfaces;
using Services.Interfaces.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Classes.Builders.SqlObjectBuilders
{
    public class LogTableUpdateBuilder : UpdateSqlObjectBuilder
    {
        private IList<IBaseSqlCommand> _commands;
        public LogTableUpdateBuilder()
        {
            _commands = new List<IBaseSqlCommand>()
            {
                new LogTableCreateCommand(new LogTableCreateDropParameter()),
                new ActivationProcedureCreateUpdateCommand(new ActivationProcedureCreateParameter())
            };
        }
        public override IList<IBaseSqlCommand> BuildCreateCommands()
        {
            return _commands;
        }
        public override IList<IBaseSqlCommand> BuildDropCommands()
        {
            return new List<IBaseSqlCommand>()
            {
                 new LogTableDropCommand(new LogTableCreateDropParameter())
            };
        }
    }
}

