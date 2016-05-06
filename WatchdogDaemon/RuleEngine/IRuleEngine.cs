using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace Watchdog.RuleEngine
{
    public interface IRuleEngine
    {
        Alert ConsumeMessage(Rule rule, Message message);
    }
}
