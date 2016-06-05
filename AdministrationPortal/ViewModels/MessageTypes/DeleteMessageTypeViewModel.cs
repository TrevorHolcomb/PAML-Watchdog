using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.MessageTypes
{
    public class DeleteMessageTypeViewModel : AbstractDeleteViewModel
    {
        public DeleteMessageTypeViewModel(MessageType messageTypeToDelete, bool safeToDelete)
        {
            MessageType = messageTypeToDelete;
            canDeleteThisModel(safeToDelete);
        }

        public override string ModelTypeName { get { return "Message Type"; } }
        public MessageType MessageType { get; set; }
    }
}