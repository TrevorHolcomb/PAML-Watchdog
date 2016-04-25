namespace WatchdogDatabaseAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rule()
        {
            Alerts = new HashSet<Alert>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public int? RuleCategoryId { get; set; }

        [Required]
        [StringLength(512)]
        public string RuleTrigger { get; set; }

        public int EscalationChainId { get; set; }
        public virtual EscalationChain EscalationChain { get; set; }

        public int AlertTypeId { get; set; }
        public virtual AlertType AlertType { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alert> Alerts { get; set; }

        public virtual RuleCategory RuleCategory { get; set; }

    }
}
