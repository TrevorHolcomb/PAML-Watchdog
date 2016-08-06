using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace AdministrationPortal.ViewModels
{

    /// <summary>
    /// Encapsulates messages passed from the Controller to the View.
    /// </summary>
    public class IndexViewModel
    {
        public enum ActionType { None, Create, Edit, Delete, Info, Warning, Error, Use }

        protected static readonly Dictionary<ActionType, string> ActionToStyle = new Dictionary<ActionType, string>
        {
            {ActionType.None,       "danger"  },
            {ActionType.Create,     "success" },
            {ActionType.Edit,       "success" },
            {ActionType.Delete,     "success" },
            {ActionType.Use,        "success" },
            {ActionType.Info,       "info"    },
            {ActionType.Warning,    "warning" },
            {ActionType.Error,      "danger"  }
        };

        protected readonly ActionType? Action;
        protected readonly Dictionary<ActionType, string> ActionToMessage;

        public IndexViewModel()
        {
            Action = ActionType.None;
        }

        public IndexViewModel(ActionType action, string entityType="", string entityName="", string message="")
        {
            Action = action;
            ActionToMessage = new Dictionary<ActionType, string>
            {
                {ActionType.None,       ""},
                {ActionType.Create,     $"Created {entityType} {entityName}" },
                {ActionType.Delete,     $"Deleted {entityType} {entityName}" },
                {ActionType.Edit,       $"Saved changes to {entityType} {entityName}" },
                {ActionType.Info,       $"{message}" },
                {ActionType.Warning,    $"{message}" },
                {ActionType.Error,      $"{message}" }
            };
        }

        /// <summary>
        /// Gets the message passed from the last action, if any.
        /// </summary>
        /// <returns></returns>
        public virtual MvcHtmlString GetMessage()
        {
            if (ConfigurationManager.AppSettings["InfoMessagesEnabled"] == bool.FalseString ||
                Action == null || Action == ActionType.None)
                return new MvcHtmlString("");

            string style = ActionToStyle[(ActionType) Action];
            string message = ActionToMessage[(ActionType) Action];
            return new MvcHtmlString($"<div class=\"alert alert-{style}\" role=\"alert\">{message}</div>");
            
            //string state = StatusToString[(StatusType) _action];
            //return new MvcHtmlString($"<div class=\"alert alert-{state}\" role=\"alert\">{_message}</div>");
        }
    }
}