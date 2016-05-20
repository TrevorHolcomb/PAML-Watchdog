using System;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.SupportCategories
{
    public class DeleteSupportCategoryViewModel
    {
        public readonly string PageTitle = "Delete";
        public SupportCategory SupportCategory{ get; set; }
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

        public DeleteSupportCategoryViewModel canDeleteThisSupportCategory(Boolean canDelete)
        {
            PageHeaderMessage = (canDelete) ? "Are you sure you want to delete this?" : "This SupportCategory is in use and cannot be deleted.";
            deleteButtonIsEnabled = canDelete;
            return this;
        }
    }
}