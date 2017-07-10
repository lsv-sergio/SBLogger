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

    public class ErrorLogTableUpdateBuilder : UpdateSqlObjectBuilder
    {
        private IList<IBaseSqlCommand> _commands;
        public ErrorLogTableUpdateBuilder()
        {
            _commands = new List<IBaseSqlCommand>()
            {
                new ErrorLogTableCreateCommand(new ErrorLogTableCreateDropParameter()),
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
                 new ErrorLogTableDropCommand(new ErrorLogTableCreateDropParameter())
            };
        }
    }
}

