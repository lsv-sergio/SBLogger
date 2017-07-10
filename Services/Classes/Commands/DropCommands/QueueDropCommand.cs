using Services.Interfaces.Builders;
using Services.Interfaces.Builders.Parameters;

namespace Services.Classes.Commands.DropCommands
{
    public class QueueDropCommand: SpSqlCommand
    {
        public QueueDropCommand(IQueueDropParameter parameterBuilder) :
            base("sp_Drop_Queue", parameterBuilder)
        {
        }
    }
}
