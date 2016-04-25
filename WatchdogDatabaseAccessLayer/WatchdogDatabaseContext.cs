namespace WatchdogDatabaseAccessLayer
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WatchdogDatabaseContext : DbContext
    {
        public WatchdogDatabaseContext() 
            //JRR connection string
            /*: base(@"Data Source=(localdb)\v11.0;Initial Catalog=watchdog;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")*/
            //TGB connection string 
            : base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=watchdog;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {
        }

        public virtual DbSet<Alert> Alerts { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageType> MessageTypes { get; set; }
        public virtual DbSet<RuleCategory> RuleCategories { get; set; }
        public virtual DbSet<Rule> Rules { get; set; }
        public virtual DbSet<Notifyee> Notifyees { get; set; }
        public virtual DbSet<NotifyeeGroup> NotifyeeGroups { get; set; }

        public virtual DbSet<EscalationChainLink> EscalationChainLinks { get; set; }
        public virtual DbSet<EscalationChain> EscalationChains { get; set; }

        public virtual DbSet<AlertType> AlertTypes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Alert
            modelBuilder.Entity<Alert>()
                .Property(e => e.Payload)
                .IsUnicode(false);

            modelBuilder.Entity<Alert>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Alert>()
                .HasRequired(e => e.AlertType)
                .WithOptional()
                .WillCascadeOnDelete(false);

            // Alert Type
            modelBuilder.Entity<AlertType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AlertType>()
                .Property(e => e.Description)
                .IsUnicode(false);


            // Message
            modelBuilder.Entity<Message>()
                .Property(e => e.Server)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.Origin)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.Params)
                .IsUnicode(false);

            // Message Type
            modelBuilder.Entity<MessageType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MessageType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<MessageType>()
                .Property(e => e.RequiredParams)
                .IsUnicode(false);

            modelBuilder.Entity<MessageType>()
                .Property(e => e.OptionalParams)
                .IsUnicode(false);

            modelBuilder.Entity<MessageType>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.MessageType)
                .WillCascadeOnDelete(false);

            // Rule Category
            modelBuilder.Entity<RuleCategory>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<RuleCategory>()
                .Property(e => e.Description)
                .IsUnicode(false);

            // Rule
            modelBuilder.Entity<Rule>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Rule>()
                .Property(e => e.RuleTrigger)
                .IsUnicode(false);

            modelBuilder.Entity<Rule>()
                .HasMany(e => e.Alerts)
                .WithRequired(e => e.Rule)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rule>()
                .HasRequired(e => e.AlertType)
                .WithMany()
                .HasForeignKey(e => e.AlertTypeId)
                .WillCascadeOnDelete(false);

            // Notifyee
            modelBuilder.Entity<Notifyee>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Notifyee>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Notifyee>()
                .HasRequired(e => e.NotifyeeGroup)
                .WithMany(e => e.Notifyees)
                .WillCascadeOnDelete(false);

            // NotifyeeGroup
            modelBuilder.Entity<NotifyeeGroup>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<NotifyeeGroup>()
                .Property(e => e.Decsription)
                .IsUnicode(false);

            modelBuilder.Entity<NotifyeeGroup>()
                .HasMany(e => e.Notifyees)
                .WithRequired(e => e.NotifyeeGroup)
                .WillCascadeOnDelete(false);
        }
    }
}
