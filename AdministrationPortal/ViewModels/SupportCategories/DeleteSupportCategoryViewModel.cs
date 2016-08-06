using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.SupportCategories
{
    public class DeleteSupportCategoryViewModel : AbstractDeleteViewModel
    {
        public DeleteSupportCategoryViewModel(SupportCategory supportCategoryToDelete, bool safeToDelete)
        {
            SupportCategory = supportCategoryToDelete;
            CanDeleteThisModel(safeToDelete);
        }

        public override string ModelTypeName => "SupportCategory";
        public SupportCategory SupportCategory{ get; set; }
    }
}