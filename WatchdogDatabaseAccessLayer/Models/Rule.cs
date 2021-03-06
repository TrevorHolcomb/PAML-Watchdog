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
    
    public partial class Rule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rule()
        {
            this.DefaultSeverity = 1;
            this.Timestamp = new DateTime(621355968000000000, DateTimeKind.Unspecified);
            this.Alerts = new HashSet<Alert>();
            this.RuleCategories = new HashSet<RuleCategory>();
            this.DefaultNotes = new HashSet<DefaultNote>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RuleCreator { get; set; }
        public string Expression { get; set; }
        public int DefaultSeverity { get; set; }
        public int AlertTypeId { get; set; }
        public string MessageTypeName { get; set; }
        public int RuleCategoryId { get; set; }
        public int SupportCategoryId { get; set; }
        public string Engine { get; set; }
        public string Origin { get; set; }
        public string Server { get; set; }
        public System.DateTime Timestamp { get; set; }
        public Nullable<int> DefaultNoteId { get; set; }
        public Nullable<int> TemplatedRuleId { get; set; }
    
        public virtual MessageType MessageType { get; set; }
        public virtual AlertType AlertType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alert> Alerts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RuleCategory> RuleCategories { get; set; }
        public virtual SupportCategory SupportCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DefaultNote> DefaultNotes { get; set; }
    }
}
