using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.Rules
{
    public class IndexRulesViewModel : IndexViewModel
    {
        public IndexRulesViewModel(ActionType action, string entityName = "", string message = "") : base(action, "Rule", entityName, message) { }

        public IEnumerable<Rule> Rules{ get; set; }
    }
}