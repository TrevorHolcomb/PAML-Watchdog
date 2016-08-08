using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.Rules
{
    public class RuleCreateViewModel: RuleModifyViewModel
    {
        public Rule BuildRule(ICollection<RuleCategory> ruleCategories)
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
                RuleCategories = ruleCategories,
                Timestamp = DateTime.Now
            };
        }
    }
}