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
    public class SetupObjectBuilder : UpdateSqlObjectBuilder
    {
        public SetupObjectBuilder()
        {
        }
        public override IList<IBaseSqlCommand> BuildCreateCommands()
        {
            var messageTypeParameter = new MessageTypeCreateDropParameter();
            var logTableParameter = new LogTableCreateDropParameter();
            var errorLogTableParameter = new ErrorLogTableCreateDropParameter();
            return new List<IBaseSqlCommand>()
            {
                new MessageTypeDropCommand(messageTypeParameter),
                new MessageTypeCreateCommand(messageTypeParameter),
                new ContractDropCommand(new ContractDropParameter()),
                new ContractCreateCommand(new ContractCreateParameter()),
                new LogTableDropCommand(logTableParameter),
                new LogTableCreateCommand(logTableParameter),
                new ErrorLogTableDropCommand(errorLogTableParameter),
                new ErrorLogTableCreateCommand(errorLogTableParameter),
                new ActivationProcedureDropCommand(new ActivationProcedureDropParameter()),
                new ActivationProcedureCreateUpdateCommand(new ActivationProcedureCreateParameter()),
                new QueueDropCommand(new QueueDropParameter()),
                new QueueCreateUpdateCommand(new QueueCreateParameter()),
                new QueueServiceDropCommand(new QueueServiceDropParameter()),
                new QueueServiceCreateCommand(new QueueServiceCreateParameter())
            };
        }

        public override IList<IBaseSqlCommand> BuildDropCommands()
        {
            return new List<IBaseSqlCommand>()
            {
                new QueueServiceDropCommand(new QueueServiceDropParameter()),
                new QueueDropCommand(new QueueDropParameter()),
                new ActivationProcedureDropCommand(new ActivationProcedureDropParameter()),
                new ErrorLogTableDropCommand(new ErrorLogTableCreateDropParameter()),
                new LogTableDropCommand(new LogTableCreateDropParameter()),
                new ContractDropCommand(new ContractDropParameter()),
                new MessageTypeDropCommand(new MessageTypeCreateDropParameter())
            };
        }
    }
}

