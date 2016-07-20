﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WatchdogDatabaseContainer : DbContext
    {
        public WatchdogDatabaseContainer()
            : base("name=WatchdogDatabaseContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageType> MessageTypes { get; set; }
        public virtual DbSet<EscalationChain> EscalationChains { get; set; }
        public virtual DbSet<EscalationChainLink> EscalationChainLinks { get; set; }
        public virtual DbSet<Notifyee> Notifyees { get; set; }
        public virtual DbSet<NotifyeeGroup> NotifyeeGroups { get; set; }
        public virtual DbSet<Rule> Rules { get; set; }
        public virtual DbSet<AlertType> AlertTypes { get; set; }
        public virtual DbSet<Alert> Alerts { get; set; }
        public virtual DbSet<RuleCategory> RuleCategories { get; set; }
        public virtual DbSet<MessageTypeParameterType> MessageTypeParameterTypes { get; set; }
        public virtual DbSet<MessageParameter> MessageParameters { get; set; }
        public virtual DbSet<AlertParameter> AlertParameters { get; set; }
        public virtual DbSet<SupportCategory> SupportCategories { get; set; }
        public virtual DbSet<Engine> Engines { get; set; }
        public virtual DbSet<AlertStatus> AlertStatuses { get; set; }
        public virtual DbSet<UnvalidatedMessage> UnvalidatedMessages { get; set; }
        public virtual DbSet<UnvalidatedMessageParameter> UnvalidatedMessageParameters { get; set; }
        public virtual DbSet<AlertGroup> AlertGroups { get; set; }
        public virtual DbSet<TemplatedRule> TemplatedRules { get; set; }
        public virtual DbSet<RuleTemplate> RuleTemplates { get; set; }
    }
}
