using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatchdogDatabaseAccessLayer.Models;
using PagedList;

namespace AdministrationPortal.ViewModels.Alerts
{
    public class AlertPagingCreateView
    {
        public String sortOrder { get; set; }
        public String status { get; set; }
        public PagedList.IPagedList<WatchdogDatabaseAccessLayer.Models.Alert> pagedList { get; set; }

        public AlertPagingCreateView(PagedList.IPagedList<WatchdogDatabaseAccessLayer.Models.Alert> pagedList, String status, String sortOrder)
        {
            this.pagedList = pagedList;
            this.status = status;
            this.sortOrder = sortOrder;
        }
    }
}