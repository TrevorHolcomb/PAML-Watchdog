using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer;

namespace WatchdogDaemon
{
    public abstract class IRuleEngine
    {
        public abstract ICollection<Alert> ConsumeMessages(ICollection<Rule> rules, ICollection<Message> messages);
        public abstract Alert ConsumeMessage(Rule rule, Message message);
    }
}
