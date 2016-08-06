using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.Rules
{
    public class RuleEditViewModel
    {
        public readonly int MAX_DEFAULTNOTES = 5;

        public int Id { get; set; }
        [Required]
        public int AlertTypeId { get; set; }
        [Required]
        public string MessageTypeName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Engine { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Server { get; set; }
        [Required]
        public int DefaultSeverity { get; set; }
        [Required]
        public string Expression { get; set; }
        [Required]
        public string RuleCreator { get; set; }
        [Required]
        public int SupportCategoryId { get; set; }
        [Required]
        public List<int> SelectedRuleCategoryIds { get; set; }

        //multiple note support
        public List<bool> SelectedNotesEnabled { get; set; }
        public List<int> SelectedNoteIds { get; set; }
        public List<bool> NewNotesEnabled { get; set; }
        public List<string> NewDefaultNotes { get; set; }
        public RuleOptionsViewModel RuleOptions { get; internal set; }


        //default note edit support
        public List<DefaultNote> DefaultNotes { get; set; }

        public RuleEditViewModel()
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

        public void Map(Rule rule)
        {
            rule.Name = Name;
            rule.Description = Description;
            rule.Expression = Expression;
            rule.Server = Server;
            rule.Origin = Origin;
            rule.DefaultSeverity = DefaultSeverity;
        }
    }

}