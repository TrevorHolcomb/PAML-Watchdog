using System.Web.Mvc;

namespace AdministrationPortal.ViewModels.Rules
{
    public class RuleExpressionViewModel
    {
        public string Variable { get; set; }
        public SelectList Operator { get; set; }
        public string Value { get; set; }
    }
}