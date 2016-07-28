using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.RuleTemplates
{
    public class DetailsRuleTemplateViewModel
    {
        [Required]
        public int RuleTemplateId { get; set; }
        public RuleTemplate RuleTemplate { get; set; }
        public Dictionary<int, string> AlertTypeIdToName { get; set; }
        public Dictionary<int, string> RuleIdToRuleCategoryNames { get; set; }

        public static string FormatInputString(IEnumerable<string> stringList)
        {

            var inputFromForm = "";
            if (stringList != null && stringList.Any())
            {
                foreach (var str in stringList)
                {
                    inputFromForm += str + ", ";
                }
                inputFromForm = inputFromForm.Remove(inputFromForm.LastIndexOf(','), 1);
            }
            return inputFromForm;
        }
    }
}