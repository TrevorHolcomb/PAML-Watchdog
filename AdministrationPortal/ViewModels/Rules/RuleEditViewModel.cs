using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.Rules
{
    public class RuleEditViewModel
    {
        public int Id { get; set; }
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

        public void Map(Rule rule)
        {
            rule.Name = Name;
            rule.Description = Description;
            rule.Expression = Expression;
        }
    }

}