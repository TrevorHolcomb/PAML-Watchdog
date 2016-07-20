using System.ComponentModel.DataAnnotations;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.Rules
{
    public class RuleEditViewModel
    {
        public int Id { get; set; }
        [Required]
        public int AlertTypeId { get; set; }
        [Required]
        public string MessageTypeName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Engine { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Server { get; set; }
        [Required]
        public int DefaultSeverity { get; set; }
        [Required]
        public string Expression { get; set; }
        [Required]
        public string RuleCreator { get; set; }
        [Required]
        public int[] RuleCategoryIds { get; set; }
        [Required]
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