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
        public String Status { get; set; }
        public int PageNo { get; set; }
        public PagedList.IPagedList<WatchdogDatabaseAccessLayer.Models.Alert> PagedList { get; set; }
        public List<SelectListItem> SortSelect { get; set; }

        public AlertPagingCreateView(PagedList.IPagedList<WatchdogDatabaseAccessLayer.Models.Alert> pagedList, String status, int pageNo)
        {
            this.PagedList = pagedList;
            this.Status = status;
            this.PageNo = pageNo;
            this.SortSelect = new List<SelectListItem>();
            SortSelect.Add(new SelectListItem { Text = "Time", Value = "4", Selected = true });
            SortSelect.Add(new SelectListItem { Text = "Severity", Value = "0"});
            SortSelect.Add(new SelectListItem { Text = "Status", Value = "1" });
            SortSelect.Add(new SelectListItem { Text = "Group", Value = "2" });
            SortSelect.Add(new SelectListItem { Text = "AlertType", Value = "3" });
            SortSelect.Add(new SelectListItem { Text = "Origin", Value = "5" });
        }
    }
}