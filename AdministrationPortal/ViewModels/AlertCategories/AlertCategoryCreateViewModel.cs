using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AdministrationPortal.ViewModels.AlertCategories
{
    public class AlertCategoryCreateViewModel: IValidatableObject
    {
        public SelectList AlertTypes { get; set; }
        public List<int> SelectedAlertTypes { get; set; }
        public SelectList EngineList { get; set; }
        [Required]
        [StringLength(450)]
        public string CategoryName { get; set; }
        [Required]
        [StringLength(450)]
        public string Server { get; set; }
        [Required]
        [StringLength(450)]
        public string Origin { get; set; }
        [Required]
        [StringLength(450)]
        public string Engine { get; set; }
        

        public AlertCategoryCreateViewModel()
        {
            SelectedAlertTypes = new List<int>();
        }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (SelectedAlertTypes.Count < 1)
                results.Add(new ValidationResult("Alert Types are required."));
            return results;
        }
    }
}