using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.Rules
{
    public class RuleEditViewModel: RuleModifyViewModel
    {
        [Required]
        public int Id { get; set; }

        //default note edit support
        public List<DefaultNote> DefaultNotes { get; set; }

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