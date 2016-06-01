//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WatchdogDatabaseAccessLayer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Alert
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Alert()
        {
            this.Assignee = "N/A";
            this.AlertParameters = new HashSet<AlertParameter>();
        }
    
        public int Id { get; set; }
        public int AlertParameterId { get; set; }
        public int AlertTypeId { get; set; }
        public string EngineName { get; set; }
        public string MessageTypeName { get; set; }
        public int RuleId { get; set; }
        public string Assignee { get; set; }
        public string Notes { get; set; }
        public string Origin { get; set; }
        public string Server { get; set; }
        public int Severity { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlertParameter> AlertParameters { get; set; }
        public virtual AlertType AlertType { get; set; }
        public virtual Engine Engine { get; set; }
        public virtual MessageType MessageType { get; set; }
        public virtual Rule Rule { get; set; }
        public virtual AlertStatus AlertStatus { get; set; }
    }
}
