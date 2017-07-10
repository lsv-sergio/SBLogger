using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Classes.Commands.SBConfigCommands
{
    public class EnableSBCommand : ConfigureSBCommand
    {
        public EnableSBCommand() : base(SBResource.cmd_EnableSB)
        {
        }
    }
}
