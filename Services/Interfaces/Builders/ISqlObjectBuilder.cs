using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces.Builders
{
    public interface ISqlObjectBuilder
    {
        IList<IBaseSqlCommand> BuildCreateCommands();
        IList<IBaseSqlCommand> BuildDropCommands();
    }
}
