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
            Bind<Repository<Alert>>().To<ListAlertRepository>().InSingletonScope();
            Bind<Repository<AlertType>>().To<ListAlertTypeRepository>().InSingletonScope();
            Bind<Repository<Engine>>().To<ListEngineRepository>().InSingletonScope();
            Bind<Repository<EscalationChainLink>>().To<ListEscalationChainLinkRepository>().InSingletonScope();
            Bind<Repository<EscalationChain>>().To<ListEscalationChainRepository>().InSingletonScope();
            Bind<Repository<MessageParameter>>().To<ListMessageParameterRepository>().InSingletonScope();
            Bind<Repository<Message>>().To<ListMessageRepository>().InSingletonScope();
            Bind<Repository<MessageTypeParameterType>>().To<ListMessageTypeParameterTypeRepository>().InSingletonScope();
            Bind<Repository<MessageType>>().To<ListMessageTypeRepository>().InSingletonScope();
            Bind<Repository<NotifyeeGroup>>().To<ListNotifyeeGroupRepository>().InSingletonScope();
            Bind<Repository<Notifyee>>().To<ListNotifyeeRepository>().InSingletonScope();
            Bind<Repository<RuleCategory>>().To<ListRuleCategoryRepository>().InSingletonScope();
            Bind<Repository<Rule>>().To<ListRuleRepository>().InSingletonScope();
        }
    }
}