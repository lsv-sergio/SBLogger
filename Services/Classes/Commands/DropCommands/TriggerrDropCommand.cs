using Services.Interfaces.Builders;
using Services.Interfaces.Builders.Parameters;

namespace Services.Classes.Commands.DropCommands
{
    public class TriggerrDropCommand : SpSqlCommand
    {
        public TriggerrDropCommand(ITriggerDropParameter parameterBuilder) :
            base("sp_Drop_Trigger", parameterBuilder)
        {
        }
    }
}
