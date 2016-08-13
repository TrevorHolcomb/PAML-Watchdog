using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDatabaseAccessLayer.Models
{
    public partial class WatchdogDatabaseContainer
    {
        public void DeleteAll()
        {
            Messages.ToList().RemoveAll(e => true);
            MessageTypes.ToList().RemoveAll(e => true);
            EscalationChains.ToList().RemoveAll(e => true);
            EscalationChainLinks.ToList().RemoveAll(e => true);
            Notifyees.ToList().RemoveAll(e => true);
            NotifyeeGroups.ToList().RemoveAll(e => true);
            Rules.ToList().RemoveAll(e => true);
            AlertTypes.ToList().RemoveAll(e => true);
            Alerts.ToList().RemoveAll(e => true);
            RuleCategories.ToList().RemoveAll(e => true);
            MessageTypeParameterTypes.ToList().RemoveAll(e => true);
            MessageParameters.ToList().RemoveAll(e => true);
            AlertParameters.ToList().RemoveAll(e => true);
            SupportCategories.ToList().RemoveAll(e => true);
            Engines.ToList().RemoveAll(e => true);
            AlertStatuses.ToList().RemoveAll(e => true);
            UnvalidatedMessages.ToList().RemoveAll(e => true);
            UnvalidatedMessageParameters.ToList().RemoveAll(e => true);
            AlertGroups.ToList().RemoveAll(e => true);
            TemplatedRules.ToList().RemoveAll(e => true);
            RuleTemplates.ToList().RemoveAll(e => true);
            DefaultNotes.ToList().RemoveAll(e => true);
            AlertCategoryItems.ToList().RemoveAll(e => true);
            AlertCategories.ToList().RemoveAll(e => true);
            SaveChanges();
        }
    }
}
