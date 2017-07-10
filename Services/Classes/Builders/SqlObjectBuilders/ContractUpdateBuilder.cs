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
    public class ContractUpdateBuilder : UpdateSqlObjectBuilder
    {
        private IList<IBaseSqlCommand> _commands;
        public ContractUpdateBuilder()
        {
            _commands = new List<IBaseSqlCommand>()
            {
                new QueueServiceDropCommand(new QueueServiceDropParameter()),
                new ContractCreateCommand(new ContractCreateParameter()),
                new QueueServiceCreateCommand(new QueueServiceCreateParameter()),
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
                new ContractDropCommand(new ContractDropParameter())
            };
        }
    }
}
