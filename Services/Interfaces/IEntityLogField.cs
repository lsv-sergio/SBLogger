using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IEntityLogField
    {
        int FieldId { get; set; }
        string FieldName { get; set; }
        ILinkEntity LinkEntity { get; set; }
    }
}
