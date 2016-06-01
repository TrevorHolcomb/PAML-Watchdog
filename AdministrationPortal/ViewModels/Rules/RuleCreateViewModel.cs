using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.Rules
{
    public class RuleCreateViewModel
    {
        public int AlertTypeId { get; set; }
        public string MessageTypeName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DefaultSeverity { get; set; }
        public string Expression { get; set; }
        public string RuleCreator { get; set; }
        public int[] RuleCategoryIds { get; set; }

        public int SupportCategoryId { get; set; }

        public RuleOptionsViewModel RuleOptions { get; set; }

        public void Map(Rule rule, IEnumerable<RuleCategory> ruleCategories)
        {
            rule.AlertTypeId = AlertTypeId;
            rule.DefaultSeverity = DefaultSeverity;
            rule.Description = Description;
            rule.Expression = Expression;
            rule.MessageTypeName = MessageTypeName;
            rule.Name = Name;
            rule.RuleCreator = RuleCreator;
            rule.SupportCategoryId = SupportCategoryId;
            rule.RuleCategories = ruleCategories.Where(e => RuleCategoryIds.Contains(e.Id)).ToList();
        }
    }
}