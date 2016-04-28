using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatchdogDatabaseAccessLayer;

namespace AdministrationPortal.ViewModels
{
    public class NotifyeesIndexViewModel
    {
        public List<Notifyee> Notifyees { get; set; }
        public List<NotifyeeGroup> NotifyeeGroups { get; set; }
    }
}