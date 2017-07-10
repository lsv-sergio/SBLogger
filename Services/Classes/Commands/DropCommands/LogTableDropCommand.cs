using Services.Interfaces.Builders;
using Services.Interfaces.Builders.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Classes.Commands.DropCommands
{
    public class LogTableDropCommand: SpSqlCommand
    {
        public LogTableDropCommand(ILogTableCreateDropParameter parameterBuilder) :
            base("sp_Drop_LogTable", parameterBuilder)
        {
        }
    }
}
