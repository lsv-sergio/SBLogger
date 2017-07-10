using Services.Enums;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Classes
{
    public class TriggerConfigParameters : ITriggerConfigParameters
    {
        IEntityLog _entityForLog;
        public TriggerConfigParameters(IEntityLog entityForLog)
        {
            IEntityLog _entityForLog = entityForLog;
        }
        public TriggerEventsEnum[] TriggerEvents { get; set; }
        public string ServiceName { get; set; }
        public string ContractName { get; set; }
        public string MessageType { get; set; }
        public IEntityLog EntityForLog { get
            {
                return _entityForLog;
            }
        }
        public string GetTriggerName()
        {
            if (!IsValid)
            {
                throw new ArgumentNullException();
            }
            var triggerTemplate = "tr{0}Logger_A{0}";
            return String.Format(triggerTemplate, this.EntityForLog.TableName, JoinEvents("", GetEventsLetter()));
        }
        public string GetTriggerActions()
        {
            return JoinEvents(", ", GetEventsStr());
        }
        public string GetTriggerFields()
        {
            return _entityForLog.GetLogFieldsStr();
        }
        public bool IsValid
        {
            get
            {
                return _entityForLog != null 
                    && !String.IsNullOrEmpty(ContractName)
                    && !String.IsNullOrEmpty(MessageType)
                    && !String.IsNullOrEmpty(ServiceName)
                    && !String.IsNullOrEmpty(TableName)
                    && TriggerEvents.Count(el => el > 0) > 0;
            }
        }
        private string JoinEvents(string delimiter, IList<string> collection)
        {
            return String.Join(delimiter, collection);
        }
        private IList<string> GetEventsLetter()
        {
            var events = GetEventsStr();
            return events.Select(evnt => evnt.Substring(0, 1)).ToList();
        }
        private IList<string> GetEventsStr()
        {
            var events = TriggerEvents
                .Where(el => el > 0)
                .OrderBy(el => el)
                .Select(el => Enum.GetName(typeof(TriggerEventsEnum), el).ToUpper()).ToArray();
            return events.ToList();
        }
    }
}
