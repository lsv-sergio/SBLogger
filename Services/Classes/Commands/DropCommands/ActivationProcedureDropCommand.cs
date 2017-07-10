using Services.Interfaces.Builders.Parameters;

namespace Services.Classes.Commands.DropCommands
{
    public class ActivationProcedureDropCommand : SpSqlCommand
    {
        public ActivationProcedureDropCommand(IActivationProcedureDropParameter parameterBuilder) :
            base("sp_Drop_ActivationProcedure", parameterBuilder)
        {
        }
    }
}
