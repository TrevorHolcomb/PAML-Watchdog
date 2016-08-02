using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.SupportCategories
{
    public class DeleteSupportCategoryViewModel : AbstractDeleteViewModel
    {
        public DeleteSupportCategoryViewModel(SupportCategory supportCategoryToDelete, bool safeToDelete)
        {
            SupportCategory = supportCategoryToDelete;
            canDeleteThisModel(safeToDelete);
        }

        public override string ModelTypeName { get { return "SupportCategory"; } }
        public SupportCategory SupportCategory{ get; set; }
    }
}