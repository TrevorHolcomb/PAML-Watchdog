using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.AlertCategories
{
    public class IndexAlertCategoriesViewModel : IndexViewModel
    {
        public IndexAlertCategoriesViewModel(ActionType action, string entityName = "", string message = "") : base(action, "Alert Category", entityName, message) { }

        public IEnumerable<AlertCategory> AlertCategories{ get; set; }
    }
}