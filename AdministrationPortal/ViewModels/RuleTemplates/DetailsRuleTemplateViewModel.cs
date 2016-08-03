using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.RuleTemplates
{
    public class DetailsRuleTemplateViewModel
    {
        [Required]
        public int RuleTemplateId { get; set; }
        public RuleTemplate RuleTemplate { get; set; }
        public Dictionary<int, string> AlertTypeIdToName { get; set; }
        public Dictionary<int, string> RuleIdToRuleCategoryNames { get; set; }
    }
}