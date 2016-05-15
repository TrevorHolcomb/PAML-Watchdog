using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogUserPortal.ViewModels.Alerts
{
    public class AlertCreateViewModel
    {
        public Alert Alert { get; set;}

        public AlertCreateViewModel(Alert Alert)
        {
            this.Alert = Alert;
        }
    }
}