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
    
    public partial class AlertGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AlertGroup()
        {
            this.Resolved = false;
            this.Alerts = new HashSet<Alert>();
        }
    
        public int Id { get; set; }
        public string Server { get; set; }
        public string Engine { get; set; }
        public string Origin { get; set; }
        public string AlertType { get; set; }
        public bool Resolved { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alert> Alerts { get; set; }
    }
}
