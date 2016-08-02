using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.RuleTemplates
{
    public class EditRuleTemplateViewModel : CreateRuleTemplateViewModel
    {
        public int Id { get; set; }
        public List<TemplatedRule> TemplatedRules { get; set; }
        public List<bool> TemplatedRulesIncluded { get; set; }
    }
}