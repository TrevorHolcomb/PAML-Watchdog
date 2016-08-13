using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDatabaseAccessLayer.Models
{
    public partial class WatchdogDatabaseContainer
    {
        private void ClearDatabase()
        {
            // disable all foreign keys
            //context.Database.ExecuteSqlCommand("EXEC sp_MSforeachtable @command1 = 'ALTER TABLE ? NOCHECK CONSTRAINT all'");

            List<string> tableNames = Database.SqlQuery<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE '%Migration%'").ToList();

            for (int i = 0; tableNames.Count > 0; i++)
            {
                try
                {
                    Database.ExecuteSqlCommand($"DELETE FROM {tableNames.ElementAt(i%tableNames.Count)}");
                    tableNames.RemoveAt(i % tableNames.Count);
                    i = 0;
                }
                catch { } // ignore errors as these are expected due to linked foreign key data             
            }


            SaveChanges();
        }

        public void DeleteAll()
        {
            
            /*RuleCategories.RemoveRange(RuleCategories.ToList());
            AlertCategoryItems.RemoveRange(AlertCategoryItems.ToList());
            AlertCategories.RemoveRange(AlertCategories.ToList());
            Alerts.RemoveRange(Alerts.ToList());
            Messages.RemoveRange(Messages.ToList());
            MessageTypes.RemoveRange(MessageTypes.ToList());
            EscalationChains.RemoveRange(EscalationChains.ToList());
            EscalationChainLinks.RemoveRange(EscalationChainLinks.ToList());
            Notifyees.RemoveRange(Notifyees.ToList());
            NotifyeeGroups.RemoveRange(NotifyeeGroups.ToList());
            Rules.RemoveRange(Rules.ToList());
            AlertTypes.RemoveRange(AlertTypes.ToList());
            RuleCategories.RemoveRange(RuleCategories.ToList());
            MessageTypeParameterTypes.RemoveRange(MessageTypeParameterTypes.ToList());
            MessageParameters.RemoveRange(MessageParameters.ToList());
            AlertParameters.RemoveRange(AlertParameters.ToList());
            SupportCategories.RemoveRange(SupportCategories.ToList());
            Engines.RemoveRange(Engines.ToList());
            AlertStatuses.RemoveRange(AlertStatuses.ToList());
            UnvalidatedMessages.RemoveRange(UnvalidatedMessages.ToList());
            UnvalidatedMessageParameters.RemoveRange(UnvalidatedMessageParameters.ToList());
            AlertGroups.RemoveRange(AlertGroups.ToList());
            TemplatedRules.RemoveRange(TemplatedRules.ToList());
            RuleTemplates.RemoveRange(RuleTemplates.ToList());
            DefaultNotes.RemoveRange(DefaultNotes.ToList());

            SaveChanges();*/
            ClearDatabase();
        }
    }
}
