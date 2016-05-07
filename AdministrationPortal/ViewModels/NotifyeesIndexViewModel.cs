using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatchdogDatabaseAccessLayer;
using WatchdogDatabaseAccessLayer.Models;

namespace AdministrationPortal.ViewModels
{
    public class NotifyeesIndexViewModel
    {
        public List<Notifyee> Notifyees { get; set; }
        public List<NotifyeeGroup> NotifyeeGroups { get; set; }
    }
}