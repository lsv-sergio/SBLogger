using Services.Classes.Builders.Parameter;
using Services.Interfaces;
using Services.Interfaces.Builders;
using Services.Interfaces.Builders.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Classes.Commands.CreateCommands
{
    public class QueueServiceCreateCommand: SpSqlCommand
    {
        public QueueServiceCreateCommand(IQueueServiceCreateParameter parameterBuilder) :
            base("sp_Create_ServiceQueue", parameterBuilder)
        {
        }
    }
}
