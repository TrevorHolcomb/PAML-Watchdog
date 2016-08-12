using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdministrationPortal.ViewModels.Rules
{
    public class RuleModifyViewModel : IValidatableObject
    {

        public readonly int MAX_DEFAULTNOTES = 5;

        [Required]
        public int AlertTypeId { get; set; }
        [Required]
        public string MessageTypeName { get; set; }
        [Required]
        [StringLength(450)]
        public string Name { get; set; }
        [Required]
        [StringLength(450)]
        public string Description { get; set; }
        [Required]
        [StringLength(450)]
        public string Engine { get; set; }
        [Required]
        [StringLength(450)]
        public string Origin { get; set; }
        [Required]
        [StringLength(450)]
        public string Server { get; set; }
        [Required]
        [Display(Name = "Default Severity")]
        public int DefaultSeverity { get; set; }
        [Required]
        [StringLength(int.MaxValue, ErrorMessage = "The Expression is required", MinimumLength = 3)]
        public string Expression { get; set; }
        [Required]
        [StringLength(450)]
        [Display(Name = "Rule Creator")]
        public string RuleCreator { get; set; }
        [Required]
        public int SupportCategoryId { get; set; }
        [Required(ErrorMessage = "A Rule Category is required")]
        [Display(Name="Rule Categories")]
        public List<int> SelectedRuleCategoryIds { get; set; }

        //multiple note support
        public List<bool> SelectedNotesEnabled { get; set; }
        public List<int> SelectedNoteIds { get; set; }
        public List<bool> NewNotesEnabled { get; set; }
        public List<string> NewDefaultNotes { get; set; }
        public RuleOptionsViewModel RuleOptions { get; internal set; }

        public RuleModifyViewModel()
        {
            SelectedNotesEnabled = new List<bool>();
            SelectedNoteIds = new List<int>();
            NewNotesEnabled = new List<bool>();
            NewDefaultNotes = new List<string>();

            PrepopulateLists();
        }

        private void PrepopulateLists()
        {
            for (int note = 0; note < MAX_DEFAULTNOTES; note++)
            {
                NewDefaultNotes.Add("");
                SelectedNoteIds.Add(0);
                NewNotesEnabled.Add(false);
                SelectedNotesEnabled.Add(false);
            }
            NewNotesEnabled[0] = true;
            SelectedNotesEnabled[0] = true;

        }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (SelectedRuleCategoryIds == null || SelectedRuleCategoryIds.Count < 1)
                results.Add(new ValidationResult("Rule Category is required"));

            if (Expression == "" || Expression.Length < 3)
                results.Add(new ValidationResult("The Expression is required"));

            return results;
        }
    }
}