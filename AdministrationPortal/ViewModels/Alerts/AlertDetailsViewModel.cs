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
        public int? PageNo { get; set; }
        public Alert Alert { get; set; }
        public List<Alert> groupedAlerts { get; set; }

        public AlertDetailsViewModel() : this(null, null, null) { }

        public AlertDetailsViewModel(Alert Alert, int? PageNo, String sortOrder)
        {
            this.Alert = Alert;
            this.PageNo = PageNo;
            this.sortOrder = sortOrder;
        }
    }
}