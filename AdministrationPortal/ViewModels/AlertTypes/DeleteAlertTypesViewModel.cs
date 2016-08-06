using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.AlertTypes
{
    public class DeleteAlertTypesViewModel : AbstractDeleteViewModel
    {
        public DeleteAlertTypesViewModel(AlertType alertTypeToDelete, bool safeToDelete)
        {
            AlertType = alertTypeToDelete;
            CanDeleteThisModel(safeToDelete);
        }

        public override string ModelTypeName => "Alert Type";
        public AlertType AlertType { get; set; }
    }
}