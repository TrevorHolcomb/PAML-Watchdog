using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.Alerts
{
    public class AlertDetailsViewModel
    {
        public String sortOrder { get; set; }
        public int PageNo { get; set; }
        public Alert Alert { get; set; }


        public AlertDetailsViewModel(Alert Alert, int PageNo, String sortOrder)
        {
            this.Alert = Alert;
            this.PageNo = PageNo;
            this.sortOrder = sortOrder;
        }
    }
}