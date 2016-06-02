using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministrationPortal.ViewModels
{
    public class EscalationChainLinksCreateViewModel
    {
        public int PreviousEscalationChainLinkId { get; set; }
        public int NotifyeeGroupId { get; set; }
    }
}