using System;

namespace AdministrationPortal.ViewModels
{
    public abstract class AbstractDeleteViewModel
    {
        public abstract string ModelTypeName { get; }
        public readonly string PageTitle = "Delete";
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

        public void canDeleteThisModel(Boolean canDelete)
        {
            PageHeaderMessage = (canDelete) ? "Are you sure you want to delete this?" : "This " + ModelTypeName + " is in use and cannot be deleted.";
            deleteButtonIsEnabled = canDelete;
        }
    }
}