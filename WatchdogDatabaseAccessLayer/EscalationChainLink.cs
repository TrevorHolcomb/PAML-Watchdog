using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDatabaseAccessLayer
{
    public class EscalationChainLink
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? NextEscalationChainLinkId { get; set; }

        public virtual EscalationChainLink NextEscalationChainLink { get; set; }

        public int NotifyeeGroupId { get; set; }

        public virtual NotifyeeGroup NotifyeeGroup { get; set; }

    }
}
