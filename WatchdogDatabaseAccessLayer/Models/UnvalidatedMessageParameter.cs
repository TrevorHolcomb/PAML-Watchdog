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
    
    public partial class UnvalidatedMessageParameter
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int MessageId { get; set; }
        public string Name { get; set; }
    
        public virtual UnvalidatedMessage Message { get; set; }
    }
}
