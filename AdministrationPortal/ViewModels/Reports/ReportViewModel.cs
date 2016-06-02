using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministrationPortal.ViewModels.Reports
{
    public class ReportViewModel
    {
        public bool AlertsPerOrigin { get; set; }
        public bool AlertsPerServer { get; set; }
        public DateTimeRangeViewModel DateTimeRange { get; set; }
    }
}