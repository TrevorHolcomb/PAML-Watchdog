using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDaemon.RuleEngine
{
    public interface IRuleEngine
    {
        bool DoesGenerateAlert(Rule rule, Message message);
    }
}
