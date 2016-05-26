using Ninject;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using WatchdogDatabaseAccessLayer.Repositories.Database;
using WatchdogDatabaseAccessLayer.Repositories.Fake;

namespace WatchdogDatabaseAccessLayer
{
    public class TestingModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<Repository<Alert>>().To<ListAlertRepository>();
            Bind<Repository<AlertType>>().To<ListAlertTypeRepository>();
            Bind<Repository<Engine>>().To<ListEngineRepository>();
            Bind<Repository<EscalationChainLink>>().To<ListEscalationChainLinkRepository>();
            Bind<Repository<EscalationChain>>().To<ListEscalationChainRepository>();
            Bind<Repository<MessageParameter>>().To<ListMessageParameterRepository>();
            Bind<Repository<Message>>().To<ListMessageRepository>();
            Bind<Repository<MessageTypeParameterType>>().To<ListMessageTypeParameterTypeRepository>();
            Bind<Repository<MessageType>>().To<ListMessageTypeRepository>();
            Bind<Repository<NotifyeeGroup>>().To<ListNotifyeeGroupRepository>();
            Bind<Repository<Notifyee>>().To<ListNotifyeeRepository>();
            Bind<Repository<RuleCategory>>().To<ListRuleCategoryRepository>();
            Bind<Repository<Rule>>().To<ListRuleRepository>();
        }
    }
}