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
            : base(@"Data Source=(localdb)\v11.0;Initial Catalog=watchdog;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
            //TGB connection string 
            /*: base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=watchdog;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")*/
        {
        }

        public virtual DbSet<Alert> Alerts { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageType> MessageTypes { get; set; }
        public virtual DbSet<RuleCategory> RuleCategories { get; set; }
        public virtual DbSet<Rule> Rules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alert>()
                .Property(e => e.Payload)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.Server)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.Origin)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.Params)
                .IsUnicode(false);

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

            modelBuilder.Entity<RuleCategory>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<RuleCategory>()
                .Property(e => e.Description)
                .IsUnicode(false);

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
        }
    }
}
