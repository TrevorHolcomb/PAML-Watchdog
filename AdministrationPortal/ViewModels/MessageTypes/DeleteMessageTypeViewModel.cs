using System;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.MessageTypes
{
    public class DeleteMessageTypeViewModel
    {
        public readonly string PageTitle = "Delete";
        public MessageType MessageType { get; set; }
        public string PageHeaderMessage { get; set; }
        private bool deleteButtonIsEnabled;

        public string ButtonClass()
        {
            return (deleteButtonIsEnabled) ? "active" : "disabled";
        }

        public string ButtonState()
        {
            return (deleteButtonIsEnabled) ? "" : "disabled=\"disabled\"";
        }

        public DeleteMessageTypeViewModel canDeleteThisMessageType(Boolean canDelete)
        {
            PageHeaderMessage = (canDelete) ? "Are you sure you want to delete this?" : "This Message Type is in use and cannot be deleted.";
            deleteButtonIsEnabled = canDelete;
            return this;
        }
    }
}