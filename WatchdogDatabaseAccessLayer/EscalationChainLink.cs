using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDatabaseAccessLayer
{
    public class EscalationChainLink
    {
        [Key, ForeignKey("")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? NextEscalationChainLinkId { get; set; }
        [ForeignKey("NextEscalationChainLinkId")]
        public virtual EscalationChainLink NextEscalationChainLink { get; set; }
        
        public int NotifyeeGroupId { get; set; }
        [ForeignKey("NotifyeeGroupId")]
        public virtual NotifyeeGroup NotifyeeGroup { get; set; }

    }
}
