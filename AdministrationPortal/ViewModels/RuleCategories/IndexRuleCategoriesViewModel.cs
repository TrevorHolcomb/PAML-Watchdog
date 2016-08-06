using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.RuleCategories
{
    public class IndexRuleCategoriesViewModel : IndexViewModel
    {
        public IndexRuleCategoriesViewModel(ActionType action, string entityName = "", string message = "") : base(action, "Rule Category", entityName, message) { }

        public IEnumerable<RuleCategory> RuleCategories { get; set; }
    }
}