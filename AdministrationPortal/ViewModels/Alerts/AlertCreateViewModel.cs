using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.Alerts
{
    public class AlertCreateViewModel
    {
        public SelectList AlertTypeIds { get; set; }
        public SelectList RuleIds { get; set; }
        public String sortOrder { get; set; }
        public int PageNo { get; set; }
        public Alert Alert { get; set; }
        

        public AlertCreateViewModel(SelectList AlertTypeIds, SelectList RuleIds)
        {
            this.AlertTypeIds = AlertTypeIds;
            this.RuleIds = RuleIds;
        }
    }
}