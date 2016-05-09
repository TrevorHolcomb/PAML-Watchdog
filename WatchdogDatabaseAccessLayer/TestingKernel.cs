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
            Bind<IRepository<Alert>>().To<ListAlertRepository>();
            Bind<IRepository<AlertType>>().To<ListAlertTypeRepository>();
            Bind<IRepository<EscalationChainLink>>().To<ListEscalationChainLinkRepository>();
            Bind<IRepository<EscalationChain>>().To<ListEscalationChainRepository>();
            Bind<IRepository<MessageParameter>>().To<ListMessageParameterRepository>();
            Bind<IRepository<Message>>().To<ListMessageRepository>();
            Bind<IRepository<MessageTypeParameterType>>().To<ListMessageTypeParameterTypeRepository>();
            Bind<IRepository<MessageType>>().To<ListMessageTypeRepository>();
            Bind<IRepository<NotifyeeGroup>>().To<ListNotifyeeGroupRepository>();
            Bind<IRepository<Notifyee>>().To<ListNotifyeeRepository>();
            Bind<IRepository<RuleCategory>>().To<ListRuleCategoryRepository>();
            Bind<IRepository<Rule>>().To<ListRuleRepository>();
        }
    }
}