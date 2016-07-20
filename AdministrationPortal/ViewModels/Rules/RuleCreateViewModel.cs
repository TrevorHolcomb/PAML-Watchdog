using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.Rules
{
    public class RuleCreateViewModel
    {
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
        public string Origin{ get; set; }
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

        public Rule BuildRule(IEnumerable<RuleCategory> ruleCategories)
        {
            return new Rule()
            {
                Engine = Engine,
                Origin = Origin,
                Server = Server,
                AlertTypeId = AlertTypeId,
                DefaultSeverity = DefaultSeverity,
                Description = Description,
                Expression = Expression,
                MessageTypeName = MessageTypeName,
                Name = Name,
                RuleCreator = RuleCreator,
                SupportCategoryId = SupportCategoryId,
                RuleCategories = ruleCategories.Where(e => RuleCategoryIds.Contains(e.Id)).ToList(),
                Timestamp = DateTime.Now
            };
        }
    }
}