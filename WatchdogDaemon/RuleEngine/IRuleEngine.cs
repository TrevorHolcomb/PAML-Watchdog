using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDaemon.RuleEngine
{
    public interface IRuleEngine
    {
        bool Evaluate(Rule rule, Message message);
    }
}
