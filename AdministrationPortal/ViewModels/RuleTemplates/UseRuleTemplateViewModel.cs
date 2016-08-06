using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace AdministrationPortal.ViewModels.RuleTemplates
{
    public class UseRuleTemplateViewModel : DetailsRuleTemplateViewModel, IValidatableObject
    {
        public SelectList RegisteredEngines { get; set; }
        [Required]
        [DisplayName("Engine")]
        public string Engine { get; set; }

        public List<string> Origins { get; set; }
        public List<string> Servers { get; set; }
        public List<KeyValuePair<string, string>> OriginServerTuples { get; set; }
        //Who instantiated this template?
        [DisplayName("Your name")]
        public string TemplateInstantiator { get; set; }

        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            results.AddRange(ValidateListCounts());
            OriginServerTuples = new List<KeyValuePair<string, string>>();
            if (results.Count == 0)
                foreach (var o in Origins.Where(o => o.Trim() != "").Select(o => o.Trim()))
                    foreach (var kvp in Servers.Where(s => s.Trim() != "").Select(s => s.Trim()).Select(s => new KeyValuePair<string,string>(o,s)))
                        if (!OriginServerTuples.Contains(kvp))
                            OriginServerTuples.Add(kvp);

            if (TemplateInstantiator == null || TemplateInstantiator.Trim() == "")
                TemplateInstantiator = "n/a";

            return results;
        }
        
        private IEnumerable<ValidationResult> ValidateListCounts()
        {
            var results = new List<ValidationResult>();

            if (Engine == null)
                results.Add(new ValidationResult("Select an Engine", new[] { "Engine" }));

            if (Origins == null || Origins.Count == 0)
                results.Add(new ValidationResult("Specify one or more Origins", new[] { "OriginsString" }));

            if (Servers == null || Servers.Count == 0)
                results.Add(new ValidationResult("Specify one or more Servers", new[] { "ServersString" }));

            return results;
        }
    }
}