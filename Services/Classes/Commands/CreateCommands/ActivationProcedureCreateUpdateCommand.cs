using Services.Classes.Builders.Parameter;
using Services.Interfaces;
using Services.Interfaces.Builders;
using Services.Interfaces.Builders.Parameters;

namespace Services.Classes.Commands.CreateCommands
{
    public class ActivationProcedureCreateUpdateCommand: SpSqlCommand
    {
        public ActivationProcedureCreateUpdateCommand(IActivationProcedureCreateParameter parameterBuilder) :
            base("sp_CreateUpdate_ActivationProcedure", parameterBuilder)
        {
        }
    }
}
