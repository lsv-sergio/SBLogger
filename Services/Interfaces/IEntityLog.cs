using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IEntityLog
    {

        string TableName { get; set; }
        IList<IEntityLogField> LogFields { get; set; }
        string IdField { get; set; }
        string ModifiedByIdField { get; set; }
        string ModifiedOnField { get; set; }
        string GetLogFieldsStr();
    }
}