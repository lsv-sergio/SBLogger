using Services.Interfaces.Builders;
using Services.Interfaces.Builders.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Classes.Commands.DropCommands
{
    class ErrorLogTableDropCommand: SpSqlCommand
    {
        public ErrorLogTableDropCommand(IErrorLogTableCreateDropParameter parameterBuilder) :
            base("sp_Drop_ErrorLogTable", parameterBuilder)
        {
        }
    }
}
