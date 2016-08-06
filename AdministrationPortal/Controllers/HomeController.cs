using System.Linq;
using System.Web.Mvc;
using AdministrationPortal.ViewModels.Home;
using Ninject;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;

namespace AdministrationPortal.Controllers
{
    public class HomeController : Controller
    {
        [Inject]
        public Repository<Alert> AlertRepository { private get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            var viewModel = new HomeViewModel()
            {
                NumberOfAlerts = AlertRepository.Get()
                    .Count(a => a.AlertStatus.StatusCode != StatusCode.Resolved)
            };

            return View(viewModel);
        }
    }
}