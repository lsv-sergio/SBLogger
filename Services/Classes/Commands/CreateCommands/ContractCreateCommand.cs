using Services.Classes.Builders.Parameter;
using Services.Interfaces;
using Services.Interfaces.Builders;
using Services.Interfaces.Builders.Parameters;

namespace Services.Classes.Commands.CreateCommands
{
    public class ContractCreateCommand : SpSqlCommand
    {
        public ContractCreateCommand(IContractCreateParameter parameterBuilder) : 
            base("sp_Create_Contract", parameterBuilder)
        {
        }
    }
}
