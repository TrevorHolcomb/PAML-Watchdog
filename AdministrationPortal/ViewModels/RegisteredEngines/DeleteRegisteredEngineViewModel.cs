using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.RegisteredEngines
{
    public class DeleteRegisteredEngineViewModel : AbstractDeleteViewModel
    {
        public DeleteRegisteredEngineViewModel(Engine engineToDelete, bool safeToDelete)
        {
            Engine = engineToDelete;
            CanDeleteThisModel(safeToDelete);
        }
        public override string ModelTypeName => "Engine";
        public Engine Engine { get; set; }
    }
}