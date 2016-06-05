using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.RegisteredEngines
{
    public class DeleteRegisteredEngineViewModel : AbstractDeleteViewModel
    {
        public DeleteRegisteredEngineViewModel(Engine engineToDelete, bool safeToDelete)
        {
            Engine = engineToDelete;
            canDeleteThisModel(safeToDelete);
        }
        public override string ModelTypeName { get { return "Engine"; } }
        public Engine Engine { get; set; }
    }
}