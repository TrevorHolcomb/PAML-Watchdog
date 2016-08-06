using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.AlertTypes
{
    public class IndexAlertTypesViewModel : IndexViewModel
    {
        public IndexAlertTypesViewModel(ActionType action, string entityName = "", string message = "") : base(action, "Alert Type", entityName, message) { }

        public IEnumerable<AlertType> AlertTypes{ get; set; }
    }
}