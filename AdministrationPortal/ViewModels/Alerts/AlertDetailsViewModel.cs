using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.Alerts
{
    public class AlertDetailsViewModel
    {
        public String SortOrder { get; set; }
        public int? PageNo { get; set; }
        public Alert Alert { get; set; }
        public List<Alert> GroupedAlerts { get; set; }

        public AlertDetailsViewModel() : this(null, null, null) { }

        public AlertDetailsViewModel(Alert alert, int? pageNo, String sortOrder)
        {
            this.Alert = alert;
            this.PageNo = pageNo;
            this.SortOrder = sortOrder;
        }
    }
}