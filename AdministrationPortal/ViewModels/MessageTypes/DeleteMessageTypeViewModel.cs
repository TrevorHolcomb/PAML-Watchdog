using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.MessageTypes
{
    public class DeleteMessageTypeViewModel : AbstractDeleteViewModel
    {
        public DeleteMessageTypeViewModel(MessageType messageTypeToDelete, bool safeToDelete)
        {
            MessageType = messageTypeToDelete;
            CanDeleteThisModel(safeToDelete);
        }

        public override string ModelTypeName => "Message Type";
        public MessageType MessageType { get; set; }
    }
}