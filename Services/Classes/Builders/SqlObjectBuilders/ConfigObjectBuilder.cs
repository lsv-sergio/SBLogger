using Services.Classes.Commands.SBConfigCommands;
using Services.Interfaces;
using System.Collections.Generic;

namespace Services.Classes.Builders.SqlObjectBuilders
{
    public class ConfigObjectBuilder : UpdateSqlObjectBuilder
    {
        public ConfigObjectBuilder()
        {
        }
        public override IList<IBaseSqlCommand> BuildCreateCommands()
        {
            return new List<IBaseSqlCommand>()
            {
                new DropConfigurationSPCommand(),
               new CreateConfigurationSPCommand()
            };
        }

        public override IList<IBaseSqlCommand> BuildDropCommands()
        {
            return new List<IBaseSqlCommand>()
            {
                new DropConfigurationSPCommand()
            };
        }
    }
}

