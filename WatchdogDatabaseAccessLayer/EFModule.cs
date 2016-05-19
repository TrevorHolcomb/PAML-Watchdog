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

            Bind<Repository<Alert>>().To<EFAlertRepository>();
            Bind<Repository<AlertType>>().To<EFAlertTypeRepository>();
            Bind<Repository<AlertParameter>>().To<EFAlertParameterRepository>();
            Bind<Repository<EscalationChainLink>>().To<EFEscalationChainLinkRepository>();
            Bind<Repository<EscalationChain>>().To<EFEscalationChainRepository>();
            Bind<Repository<MessageParameter>>().To<EFMessageParameterRepository>();
            Bind<Repository<Message>>().To<EFMessageRepository>();
            Bind<Repository<MessageTypeParameterType>>().To<EFMessageTypeParameterTypeRepository>();
            Bind<Repository<MessageType>>().To<EFMessageTypeRepository>();
            Bind<Repository<NotifyeeGroup>>().To<EFNotifyeeGroupRepository>();
            Bind<Repository<Notifyee>>().To<EFNotifyeeRepository>();
            Bind<Repository<RuleCategory>>().To<EFRuleCategoryRepository>();
            Bind<Repository<Rule>>().To<EFRuleRepository>();
            Bind<Repository<SupportCategory>>().To<EFSupportCategoryRepository>();
        }
    }
}