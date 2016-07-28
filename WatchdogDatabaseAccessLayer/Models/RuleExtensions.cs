namespace WatchdogDatabaseAccessLayer.Models
{
    public static class RuleExtensions
    {
        public static TemplatedRule ToTemplate(this Rule rule)
        {
            return new TemplatedRule()
            {
                Name = rule.Name,
                Description = rule.Description,
                AlertTypeId = rule.AlertTypeId,
                DefaultSeverity = rule.DefaultSeverity,
                Expression = rule.Expression,
                MessageTypeName = rule.MessageTypeName,
                SupportCategoryId = rule.SupportCategoryId,
                RuleCategories = rule.RuleCategories,
                RuleCreator = "template; TODO: replace me"
            };
        }

        public static bool EqualsTemplatedRule(this Rule rule, TemplatedRule templatedRule)
        {
            return rule.Name == templatedRule.Name &&
                   rule.AlertTypeId == templatedRule.AlertTypeId &&
                   rule.Expression == templatedRule.Expression &&
                   rule.MessageTypeName == templatedRule.MessageTypeName &&
                   rule.SupportCategoryId == templatedRule.SupportCategoryId;
        }

        public static bool EqualsRule(this Rule rule, Rule other)
        {
            return rule.Name == other.Name &&
                   rule.AlertTypeId == other.AlertTypeId &&
                   rule.Expression == other.Expression &&
                   rule.MessageTypeName == other.MessageTypeName &&
                   rule.SupportCategoryId == other.SupportCategoryId;
        }
    }
}