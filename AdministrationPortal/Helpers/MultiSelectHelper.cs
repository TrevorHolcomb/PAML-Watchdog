using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AdministrationPortal.Helpers
{
    public static  class MultiSelectHelper
    {
        public static MvcHtmlString MultiSelectDropDown(this HtmlHelper helper, string formInputId, MultiSelectList options, IEnumerable<int> selectedOptionsIds, string classes)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<select");
            stringBuilder.Append(@" class=""" + classes + @"""");
            stringBuilder.Append(@" id=""" + formInputId + @"""");
            stringBuilder.Append(@" name=""" + formInputId + @"""");
            stringBuilder.Append(@" multiple=""multiple"">");
            stringBuilder.AppendLine();
            
            foreach (var option in options)
                BuildOption(selectedOptionsIds, stringBuilder, option);
            stringBuilder.AppendLine("</select>");

            return new MvcHtmlString(stringBuilder.ToString());
        }
        

        private static void BuildOption(IEnumerable<int> selectedOptionsIds, StringBuilder stringBuilder, SelectListItem option)
        {
            stringBuilder.Append(@"<option value=""" + option.Value + @"""");

            stringBuilder.Append(selectedOptionsIds.Contains(int.Parse(option.Value)) ? @"selected=""selected"">" : ">");

            stringBuilder.Append(option.Text);

            stringBuilder.Append("</option>");
        }
    }
}