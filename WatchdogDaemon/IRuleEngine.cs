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
        public abstract void ConsumeMessages(ICollection<Rule> rules, ICollection<Message> messages);
        public abstract void ConsumeMessage(Rule rule, Message message);
    }
}
