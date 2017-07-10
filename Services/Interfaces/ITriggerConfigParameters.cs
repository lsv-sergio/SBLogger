using Services.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface ITriggerConfigParameters : IBaseParameter
    {
        TriggerEventsEnum[] TriggerEvents { get; set; }
        string ServiceName { get; set; }
        string ContractName { get; set; }
        string MessageType { get; set; }
        IEntityLog EntityForLog { get;}
        bool IsValid { get; }
        string GetTriggerName();
        string GetTriggerActions();
        string GetTriggerFields();
    }
}
