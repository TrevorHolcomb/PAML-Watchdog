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
    
    public partial class Message
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Message()
        {
            this.MessageParameters = new HashSet<MessageParameter>();
        }
    
        public int Id { get; set; }
        public string MessageTypeName { get; set; }
        public string Server { get; set; }
        public string Origin { get; set; }
        public string EngineName { get; set; }
        public int RuleTemplateId { get; set; }
    
        public virtual MessageType MessageType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MessageParameter> MessageParameters { get; set; }
        public virtual Engine Engine { get; set; }
    }
}
