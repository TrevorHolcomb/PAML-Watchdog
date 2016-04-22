namespace WatchdogDatabaseAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Alert
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int AlertTypeId { get; set; }

        public int RuleId { get; set; }

        [Required]
        [StringLength(512)]
        public string Payload { get; set; }

        public DateTime Timestamp { get; set; }

        public int AlertStatusId { get; set; }

        public virtual Rule Rule { get; set; }
    }
}
