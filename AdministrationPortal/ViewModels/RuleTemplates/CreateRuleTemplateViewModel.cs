using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.RuleTemplates
{
    public class CreateRuleTemplateViewModel
    {
        public List<Rule> Rules { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<bool> RulesIncluded{ get; set; }
    }
}