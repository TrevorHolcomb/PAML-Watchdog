using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchdogDatabaseAccessLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace AdministrationPortal.ViewModels.AlertCategories
{
    public class AlertCategoryEditViewModel
    {
        public SelectList AlertTypes { get; set; }
        public List<int> SelectedAlertTypes { get; set; }
        public SelectList EngineList { get; set; }
        public String CategoryName { get; set; }
        [Required]
        public String Server { get; set; }
        [Required]
        public String Origin { get; set; }
        [Required]
        public String Engine { get; set; }
        public AlertCategory AlertCategory { get; set; }

        public AlertCategoryEditViewModel()
        {
            this.SelectedAlertTypes = new List<int>();
        }
    }
}