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
    public class MessageTypeUpdateBuilder : UpdateSqlObjectBuilder
    {
        private IList<IBaseSqlCommand> _commands;
        public MessageTypeUpdateBuilder()
        {
            _commands = new List<IBaseSqlCommand>()
            {
                new QueueServiceDropCommand(new QueueServiceDropParameter()),
                new ContractDropCommand(new ContractDropParameter()),
                new MessageTypeCreateCommand(new MessageTypeCreateDropParameter()),
                new ContractCreateCommand(new ContractCreateParameter()),
                new QueueServiceCreateCommand(new QueueServiceCreateParameter())
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
                new MessageTypeDropCommand(new MessageTypeCreateDropParameter())
            };
        }
    }
}

