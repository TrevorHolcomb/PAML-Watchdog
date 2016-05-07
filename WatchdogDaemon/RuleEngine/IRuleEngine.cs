using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDaemon.RuleEngine
{
    public interface IRuleEngine
    {
        Alert ConsumeMessage(Rule rule, Message message);
    }
}
