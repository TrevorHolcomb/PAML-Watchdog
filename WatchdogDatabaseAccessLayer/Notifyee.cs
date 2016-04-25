using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchdogDatabaseAccessLayer
{
    public partial class Notifyee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        public string Email { get; set; }

        public int NotifyeeGroupId { get; set; }

        public virtual NotifyeeGroup NotifyeeGroup { get; set; }
    }
}
