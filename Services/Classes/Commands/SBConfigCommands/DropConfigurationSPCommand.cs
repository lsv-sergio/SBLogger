using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Classes.Commands.SBConfigCommands
{
    public class DropConfigurationSPCommand : ConfigureSBCommand
    {
        public DropConfigurationSPCommand(): base(SBResource.sp_Drop_ConfigurationSP)
        {
        }
    }
}
