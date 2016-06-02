using System.Web.Mvc;

namespace AdministrationPortal.ViewModels.Rules
{
    public class RuleOptionsViewModel
    {
        public SelectList AlertTypes { get; set; }
        public SelectList MessageTypes { get; set; }
        public MultiSelectList RuleCategories { get; set; }
        public SelectList SupportCategories { get; set; }
        public SelectList EngineList { get; set; }
    }
}