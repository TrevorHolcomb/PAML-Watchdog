using System.ComponentModel;
using System.Configuration;
using System.Web.Mvc;
using AdministrationPortal.ViewModels;
using NLog;

namespace AdministrationPortal.Controllers
{
    public abstract class AbstractBaseController: Controller
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Called when an unhandled exception occurs in the action.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            Logger.Error(filterContext.Exception);

            if (ConfigurationManager.AppSettings["ExceptionHandlingEnabled"].ToLower().Equals(bool.TrueString.ToLower()))
            {
                filterContext.ExceptionHandled = true;

                var exceptionType = IndexViewModel.ActionType.Error;

                if (filterContext.Exception is WarningException)
                    exceptionType = IndexViewModel.ActionType.Warning;

                // Redirect on error:
                filterContext.Result = RedirectToAction("Index", new
                {
                    actionPerformed = exceptionType,
                    message = filterContext.Exception.Message
                });
            }
        }
    }
}