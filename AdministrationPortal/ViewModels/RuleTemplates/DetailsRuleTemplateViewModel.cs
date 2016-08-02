using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.RuleTemplates
{
    public class DetailsRuleTemplateViewModel : IValidatableObject
    {
        public RuleTemplate RuleTemplate { get; set; }
        public IEnumerable<string> RuleAlertTypeNames { get; set; }
        public IEnumerable<string> RuleRuleCategoryNames { get; set; }

        [Required]
        public int RuleTemplateId { get; set; }
        public SelectList RegisteredEngines { get; set; }
        [Required]
        [DisplayName("Engine")]
        public string Engine { get; set; }
        [Required]
        [DisplayName("Origins")]
        public string OriginsString { get; set; }
        [Required]
        [DisplayName("Servers")]
        public string ServersString { get; set; }
        public List<string> Origins { get; set; }
        public List<string> Servers { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            Origins = OriginsString.Split(',').Select(o => o.Trim())
                .Where(o => !o.IsEmpty()).ToList();
            Servers = ServersString.Split(',').Select(s => s.Trim())
                .Where(s => !s.IsEmpty()).ToList();

            OriginsString = FormatInputString(Origins);
            ServersString = FormatInputString(Servers);

            var results = new List<ValidationResult>();
            results.AddRange(ValidateListCounts());    
            return results;
        }

        private static string FormatInputString(IEnumerable<string> stringList)
        {
            var inputFromForm = "";
            foreach (var s in stringList)
            {
                inputFromForm += s + ",";
            }
            inputFromForm = inputFromForm.Substring(0, inputFromForm.LastIndexOf(','));
            return inputFromForm;
        }

        private IEnumerable<ValidationResult> ValidateListCounts()
        {
            var results = new List<ValidationResult>();

            if (Engine == null)
                results.Add(new ValidationResult("Select an Engine", new[] { "Engine" } ));

            if (Origins.Count == 0)
                results.Add(new ValidationResult("Specify one or more Origins", new []{ "OriginsString" }));

            if (Servers.Count == 0)
                results.Add(new ValidationResult("Specify one or more Servers", new []{ "ServersString" }));

            if (Origins.Count > 1 && Servers.Count > 1 && Origins.Count != Servers.Count)
                results.Add(new ValidationResult("The count of Origins to Servers must be a one-to-many, many-to-one, or equivalent number of many-to-many (n Origins and n Servers) relationship.",
                    new [] {"OriginsString", "ServersString"}));

            return results;
        }

    }
}