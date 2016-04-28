using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AdministrationPortal.Helpers
{
    public static class MenuLinkHelper
    {
        /*
         * Returns "active" if the specified controller is the current controller.
         */
        public static MvcHtmlString HighlightIfActive(this HtmlHelper helper, string controller)
        {
            var routeData = helper.ViewContext.RouteData.Values;
            var currentController = routeData["controller"] as string;

            return string.Equals(controller, currentController, StringComparison.OrdinalIgnoreCase) ? new MvcHtmlString("active") : new MvcHtmlString("");
        }
    }
}