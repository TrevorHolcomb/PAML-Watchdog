//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WatchdogDatabaseAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Alert
    {
        public int Id { get; set; }
        public string Payload { get; set; }
        public System.DateTime Timestamp { get; set; }
        public int AlertTypeId { get; set; }
        public int RuleId { get; set; }
        public string Notes { get; set; }
        public AlertStatus Status { get; set; }
    
        public virtual AlertType AlertType { get; set; }
        public virtual Rule Rule { get; set; }
    }
}
