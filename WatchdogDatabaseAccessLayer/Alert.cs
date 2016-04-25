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
        [Column("Id")]
        public int Id { get; set; }

        [Column("AlertTypeId")]
        public int AlertTypeId { get; set; }
        public virtual AlertType AlertType { get; set; }

        [Column("RuleId")]
        public int RuleId { get; set; }

        [Required]
        [StringLength(512)]
        [Column("Payload")]
        public string Payload { get; set; }

        [Column("Timestamp")]
        public DateTime Timestamp { get; set; }

        [Column("AlertStatusId")]
        public int AlertStatusId { get; set; }

        public virtual Rule Rule { get; set; }
    }
}
