using Services.Interfaces.Builders;
using Services.Interfaces.Builders.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Classes.Commands.DropCommands
{
    public class QueueServiceDropCommand: SpSqlCommand
    {
        public QueueServiceDropCommand(IQueueServiceDropParameter parameterBuilder) :
            base("sp_Drop_ServiceQueue", parameterBuilder)
        {
        }
    }
}
