using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Classes.Commands.SBConfigCommands
{
    public class CreateConfigurationSPCommand : ConfigureSBCommand
    {
        public CreateConfigurationSPCommand() : base(SBResource.sp_Create_ConfigurationSP)
        {
        }
    }
}
