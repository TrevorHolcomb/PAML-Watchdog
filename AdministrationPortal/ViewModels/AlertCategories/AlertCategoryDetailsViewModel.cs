using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.AlertCategories
{
    public class AlertCategoryDetailsViewModel
    {
        public List<AlertType> AlertTypes { get; set; }
        public AlertCategory AlertCategory { get; set; }
        public String Engine { get; set; }
        public String Origin { get; set; }
        public String Server { get; set; }
    }
}