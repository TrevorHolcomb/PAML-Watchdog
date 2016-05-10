using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using WatchdogDatabaseAccessLayer.Repositories.Database;

namespace WatchdogDatabaseAccessLayer
{
    public class EFModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<WatchdogDatabaseContainer>().ToSelf().InSingletonScope();

            Bind<IRepository<Alert>>().To<EFAlertRepository>();
            Bind<IRepository<AlertType>>().To<EFAlertTypeRepository>();
            Bind<IRepository<EscalationChainLink>>().To<EFEscalationChainLinkRepository>();
            Bind<IRepository<EscalationChain>>().To<EFEscalationChainRepository>();
            Bind<IRepository<MessageParameter>>().To<EFMessageParameterRepository>();
            Bind<IRepository<Message>>().To<EFMessageRepository>();
            Bind<IRepository<MessageTypeParameterType>>().To<EFMessageTypeParameterTypeRepository>();
            Bind<IRepository<MessageType>>().To<EFMessageTypeRepository>();
            Bind<IRepository<NotifyeeGroup>>().To<EFNotifyeeGroupRepository>();
            Bind<IRepository<Notifyee>>().To<EFNotifyeeRepository>();
            Bind<IRepository<RuleCategory>>().To<EFRuleCategoryRepository>();
            Bind<IRepository<Rule>>().To<EFRuleRepository>();
        }
    }
}