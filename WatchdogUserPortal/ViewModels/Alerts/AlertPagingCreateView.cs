﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatchdogDatabaseAccessLayer.Models;
using PagedList;

namespace WatchdogUserPortal.ViewModels.Alerts
{
    public class AlertPagingCreateView
    {

        public String status { get; set; }
        public PagedList.IPagedList<WatchdogDatabaseAccessLayer.Models.Alert> pagedList { get; set; }

        public AlertPagingCreateView(PagedList.IPagedList<WatchdogDatabaseAccessLayer.Models.Alert> pagedList, String status)
        {
            this.pagedList = pagedList;
            this.status = status;
        }
    }
}