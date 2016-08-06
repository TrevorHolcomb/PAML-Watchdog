using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AdministrationPortal.Helpers
{
    public static class NavbarHelper
    {
        /*
         * Returns "active" if the specified controller is the current controller.
         */
        public static MvcHtmlString HighlightActiveAlertMode(this HtmlHelper helper, string controller, string state)
        {
            if (controller != "Alerts")
                return new MvcHtmlString("");

            var queryString = helper.ViewContext.RequestContext.HttpContext.Request.QueryString;

            if (!queryString.HasKeys() && state == "Unresolved")
                return new MvcHtmlString("active");

            var activeOrArchived = queryString.Get("ActiveOrArchived");

            if (activeOrArchived == null)
                return new MvcHtmlString("");
            
            return (activeOrArchived.ToLower().Equals(state.ToLower())) ? new MvcHtmlString("active") : new MvcHtmlString("");
        }
    }
}