using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.RuleTemplates
{
    public class ViewRuleTemplateViewModel
    {
        public IEnumerable<RuleTemplate> RuleTemplates { get; set; }
        public bool InfoMessageHidden { private get; set; } = true;
        public string InfoMessageStyle => (InfoMessageHidden) ? "display:none; " : "";
        public int NumberOfRulesInstantiated { get; set; }

        public RuleTemplate RuleTemplateInstantiated { get; set; } = new RuleTemplate()
        {
            TemplatedRules = new List<TemplatedRule>()
        };
    }
}