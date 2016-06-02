using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministrationPortal.ViewModels.Reports
{
    public class BarChartViewModel
    {
        public string[] XAxis { get; set; }
        public int[] YAxis { get; set; }
        public string Title { get; set; }

        public BarChartViewModel(string[] xAxis, int[] yAxis, string title)
        {
            Title = title;
            XAxis = xAxis;
            YAxis = yAxis;
        }
    }
}