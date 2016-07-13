using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchdogDatabaseAccessLayer.Models;
using PagedList;

namespace AdministrationPortal.ViewModels.Alerts
{
    public class AlertPagingCreateView
    {
        public String status { get; set; }
        public int PageNo { get; set; }
        public PagedList.IPagedList<WatchdogDatabaseAccessLayer.Models.Alert> pagedList { get; set; }
        public List<SelectListItem> sortSelect { get; set; }

        public AlertPagingCreateView(PagedList.IPagedList<WatchdogDatabaseAccessLayer.Models.Alert> pagedList, String status, int PageNo)
        {
            this.pagedList = pagedList;
            this.status = status;
            this.PageNo = PageNo;
            this.sortSelect = new List<SelectListItem>();
            sortSelect.Add(new SelectListItem { Text = "Time", Value = "4", Selected = true });
            sortSelect.Add(new SelectListItem { Text = "Severity", Value = "0"});
            sortSelect.Add(new SelectListItem { Text = "Status", Value = "1" });
            sortSelect.Add(new SelectListItem { Text = "Group", Value = "2" });
            sortSelect.Add(new SelectListItem { Text = "AlertType", Value = "3" });
            sortSelect.Add(new SelectListItem { Text = "Origin", Value = "5" });
        }
    }
}