using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.RuleCategories
{
    public class DeleteRuleCategoryViewModel : AbstractDeleteViewModel
    {
        public DeleteRuleCategoryViewModel(RuleCategory toDelete, bool safeToDelete)
        {
            RuleCategory = toDelete;
            CanDeleteThisModel(safeToDelete);
        }
        public override string ModelTypeName { get { return "Rule Category"; } }
        public RuleCategory RuleCategory { get; set; }
    }
}