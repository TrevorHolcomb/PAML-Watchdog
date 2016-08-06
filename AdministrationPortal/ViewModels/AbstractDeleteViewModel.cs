namespace AdministrationPortal.ViewModels
{
    public abstract class AbstractDeleteViewModel
    {
        public abstract string ModelTypeName { get; }
        public readonly string PageTitle = "Delete";
        public string PageHeaderMessage { get; set; }
        private bool _deleteButtonIsEnabled;
        
        public string ButtonClass()
        {
            return (_deleteButtonIsEnabled) ? "active" : "disabled";
        }

        public string ButtonState()
        {
            return (_deleteButtonIsEnabled) ? "" : "disabled=\"disabled\"";
        }

        public void CanDeleteThisModel(bool canDelete)
        {
            PageHeaderMessage = (canDelete) ? "Are you sure you want to delete this?" : "This " + ModelTypeName + " is in use and cannot be deleted.";
            _deleteButtonIsEnabled = canDelete;
        }
    }
}