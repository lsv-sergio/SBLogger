using Services.Interfaces.Builders;
using Services.Interfaces.Builders.Parameters;

namespace Services.Classes.Commands.DropCommands
{
    public class ContractDropCommand : SpSqlCommand
    {
        public ContractDropCommand(IContractDropParameter parameterBuilder) :
            base("sp_Drop_Contract", parameterBuilder)
        {
        }
    }
}
