using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdministrationPortal.ViewModels.Reports;
using Ninject;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;

namespace AdministrationPortal.Controllers
{
    public class ReportsController : Controller
    {
        [Inject]
        public Repository<Alert> AlertRepository { private get; set; }

        public ActionResult Index()
        {
            return View();
        }

       

        // GET: Reports
        public ViewResult Report(ReportViewModel viewModel)
        {
            return View(viewModel);
        }

        private static bool InRange(DateTime datetime, DateTime start, DateTime end)
        {
            return start < datetime && datetime < end;
        }

        
        public ActionResult AlertsPerOrigin(DateTimeRangeViewModel range)
        {
            var alerts = AlertRepository.Get().Where(alert => InRange(alert.TimeCreated, range.Start, range.End)).ToList();

            var origins = alerts.Select(e => e.Origin).Distinct().ToArray();
            var values = new int[origins.Length];
            for (var i = 0; i < origins.Length; i++)
            {
                values[i] = alerts.Count(alert => string.Compare(alert.Origin, origins[i], StringComparison.OrdinalIgnoreCase) == 0);
            }

            return View("BarChart",new BarChartViewModel(origins, values, "Alerts Per Origin"));
        }

        public ActionResult AlertsPerServer(DateTimeRangeViewModel range)
        {
            var alerts = AlertRepository.Get().Where(alert => InRange(alert.TimeCreated, range.Start, range.End)).ToList();

            var servers = alerts.Select(e => e.Server).Distinct().ToArray();
            var values = new int[servers.Length];
            for (var i = 0; i < servers.Length; i++)
            {
                values[i] = alerts.Count(alert => string.Compare(alert.Server, servers[i], StringComparison.OrdinalIgnoreCase) == 0);
            }

            return View("BarChart", new BarChartViewModel(servers, values, "Alerts Per Server"));
        }
    }
}