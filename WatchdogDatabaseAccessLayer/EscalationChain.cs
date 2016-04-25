using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDatabaseAccessLayer
{
    public class EscalationChain
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public int EscalationChainLinkId { get; set; }
        public virtual EscalationChainLink EscalationChainLink { get; set; }
    }
}