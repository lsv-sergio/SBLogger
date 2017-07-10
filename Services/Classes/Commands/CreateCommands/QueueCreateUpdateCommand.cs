using Services.Interfaces;
using Services.Interfaces.Builders;
using Services.Interfaces.Builders.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Classes.Commands.CreateCommands
{
    public class QueueCreateUpdateCommand: SpSqlCommand
    {
        public QueueCreateUpdateCommand(IQueueCreateParameter parameterBuilder) :
            base("sp_CreateUpdate_Queue", parameterBuilder)
        {
        }
    }
}
