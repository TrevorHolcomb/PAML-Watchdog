using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels.Alerts
{
    public class AlertCreateViewModelUserPortal
    {
        public Alert Alert { get; set; }

        public AlertCreateViewModelUserPortal(Alert Alert)
        {
            this.Alert = Alert;
        }
    }
}