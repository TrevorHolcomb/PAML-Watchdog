using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.SupportCategories
{
    public class IndexSupportCategoryViewModel : IndexViewModel
    {
        public IndexSupportCategoryViewModel(ActionType action, string entityName="", string message="") : base(action, "Support Category", entityName, message){}

        public IEnumerable<SupportCategory> SupportCategories { get; set; }
    }
}