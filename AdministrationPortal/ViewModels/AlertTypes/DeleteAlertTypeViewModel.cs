using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.AlertTypes
{
    public class DeleteAlertTypeViewModel : AbstractDeleteViewModel
    {
        public DeleteAlertTypeViewModel(AlertType alertTypeToDelete, bool safeToDelete)
        {
            AlertType = alertTypeToDelete;
            CanDeleteThisModel(safeToDelete);
        }

        public override string ModelTypeName { get { return "Alert Type"; } }
        public AlertType AlertType { get; set; }
    }
}