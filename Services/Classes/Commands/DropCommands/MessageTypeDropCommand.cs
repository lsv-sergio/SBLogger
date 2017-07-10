using Services.Interfaces.Builders;
using Services.Interfaces.Builders.Parameters;

namespace Services.Classes.Commands.DropCommands
{
    public class MessageTypeDropCommand: SpSqlCommand
    {
        public MessageTypeDropCommand(IMessageTypeCreateDropParameter parameterBuilder) :
            base("sp_Drop_MessageType", parameterBuilder)
        {
        }
    }
}
