namespace WatchdogDatabaseAccessLayer.Models
{
    public static class RuleExtensions
    {
        public static TemplatedRule ToTemplate(this Rule rule)
        {
            var templatedRule = new TemplatedRule();
            templatedRule.Name = rule.Name;
            templatedRule.Description = rule.Description;            
            templatedRule.AlertTypeId = rule.AlertTypeId;
            templatedRule.DefaultSeverity = rule.DefaultSeverity;
            templatedRule.Expression = rule.Expression;
            templatedRule.MessageTypeName = rule.MessageTypeName;
            templatedRule.SupportCategoryId = rule.SupportCategoryId;
            templatedRule.RuleCreator = "template; TODO: replace me";
            return templatedRule;
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