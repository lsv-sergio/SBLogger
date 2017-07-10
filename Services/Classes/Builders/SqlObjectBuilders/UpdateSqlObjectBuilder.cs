using Services.Interfaces.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Services.Interfaces;

namespace Services.Classes.Builders.SqlObjectBuilders
{
    public abstract class UpdateSqlObjectBuilder : ISqlObjectBuilder
    {
        public abstract IList<IBaseSqlCommand> BuildCreateCommands();

        public abstract IList<IBaseSqlCommand> BuildDropCommands();
    }
}
