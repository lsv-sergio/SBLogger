using Services.Classes.Builders.Parameter;
using Services.Interfaces;
using Services.Interfaces.Builders;
using Services.Interfaces.Builders.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Classes.Commands.CreateCommands
{
    class ErrorLogTableCreateCommand: SpSqlCommand
    {
        public ErrorLogTableCreateCommand(IErrorLogTableCreateDropParameter parameterBuilder) :
            base("sp_Create_ErrorLogTable", parameterBuilder)
        {
        }
    }
}
