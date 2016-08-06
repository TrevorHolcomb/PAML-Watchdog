using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.RegisteredEngines
{
    public class IndexRegisteredEnginesViewModel : IndexViewModel
    {
        public IndexRegisteredEnginesViewModel(ActionType action, string entityName = "", string message = "") : base(action, "Engine", entityName, message) { }

        public IEnumerable<Engine> Engines { get; set; }
    }
}