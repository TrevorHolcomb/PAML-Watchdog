using System.Web.Mvc;

namespace AdministrationPortal.ViewModels
{
    public class RuleOptionsViewModel
    {
        public SelectList AlertTypes { get; set; }
        public SelectList EscalationChains { get; set; }
        public SelectList MessageTypes { get; set; }
        public SelectList RuleCategories { get; set; }
        public SelectList SupportCategories { get; set; }
    }
}