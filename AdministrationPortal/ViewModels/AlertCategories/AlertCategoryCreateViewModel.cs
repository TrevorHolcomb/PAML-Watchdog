using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchdogDatabaseAccessLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace AdministrationPortal.ViewModels.AlertCategories
{
    public class AlertCategoryCreateViewModel
    {
        public SelectList AlertTypes { get; set; }
        public SelectList SelectedAlertTypes { get; set; }
        public SelectList EngineList { get; set; }
        public String CategoryName { get; set; }
        [Required]
        public String Server { get; set; }
        [Required]
        public String Origin { get; set; }

        public AlertCategoryCreateViewModel()
        {
           
        }
    }
}