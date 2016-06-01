using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministrationPortal.ViewModels.Reports
{
    public class DateTimeRangeViewModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTimeRangeViewModel(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTimeRangeViewModel()
        {
        }
    }
}