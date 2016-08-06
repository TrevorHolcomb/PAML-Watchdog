using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.MessageTypes
{
    public class IndexMessageTypesViewModel : IndexViewModel
    {
        public IndexMessageTypesViewModel(ActionType action, string entityName = "", string message = "") : base(action, "Message Type", entityName, message) { }

        public IEnumerable<MessageType> MessageTypes { get; set; }
    }
}