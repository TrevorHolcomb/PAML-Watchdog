using Ninject.Web.Common;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using WatchdogDatabaseAccessLayer.Repositories.Database;

namespace WatchdogDatabaseAccessLayer
{
    public class EFModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<WatchdogDatabaseContainer>().To<WatchdogDatabaseContainer>().InThreadScope();

            Bind<Repository<Alert>>().To<EFAlertRepository>();
            Bind<Repository<AlertType>>().To<EFAlertTypeRepository>();
            Bind<Repository<AlertStatus>>().To<EFAlertStatusRepository>();
            Bind<Repository<AlertParameter>>().To<EFAlertParameterRepository>();
            Bind<Repository<Engine>>().To<EFEngineRepository>();
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
            Bind<Repository<RuleTemplate>>().To<EFRuleTemplateRepository>();
            Bind<Repository<SupportCategory>>().To<EFSupportCategoryRepository>();
            Bind<Repository<TemplatedRule>>().To<EFTemplatedRuleRepository>();
            Bind<Repository<UnvalidatedMessage>>().To<EFUnvalidatedMessageRepository>();
            Bind<Repository<UnvalidatedMessageParameter>>().To<EFUnvalidatedMessageParameterRepository>();
            Bind<Repository<AlertGroup>>().To<EFAlertGroupRepository>();
            Bind<Repository<DefaultNote>>().To<EFDefaultNoteRepositroy>();
        }
    }

    public class EFWebModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<WatchdogDatabaseContainer>().To<WatchdogDatabaseContainer>().InRequestScope();

            Bind<Repository<Alert>>().To<EFAlertRepository>().InRequestScope();
            Bind<Repository<AlertType>>().To<EFAlertTypeRepository>().InRequestScope();
            Bind<Repository<AlertStatus>>().To<EFAlertStatusRepository>().InRequestScope();
            Bind<Repository<AlertParameter>>().To<EFAlertParameterRepository>().InRequestScope();
            Bind<Repository<Engine>>().To<EFEngineRepository>().InRequestScope();
            Bind<Repository<EscalationChainLink>>().To<EFEscalationChainLinkRepository>().InRequestScope();
            Bind<Repository<EscalationChain>>().To<EFEscalationChainRepository>().InRequestScope();
            Bind<Repository<MessageParameter>>().To<EFMessageParameterRepository>().InRequestScope();
            Bind<Repository<Message>>().To<EFMessageRepository>().InRequestScope();
            Bind<Repository<MessageTypeParameterType>>().To<EFMessageTypeParameterTypeRepository>().InRequestScope();
            Bind<Repository<MessageType>>().To<EFMessageTypeRepository>().InRequestScope();
            Bind<Repository<NotifyeeGroup>>().To<EFNotifyeeGroupRepository>().InRequestScope();
            Bind<Repository<Notifyee>>().To<EFNotifyeeRepository>().InRequestScope();
            Bind<Repository<RuleCategory>>().To<EFRuleCategoryRepository>().InRequestScope();
            Bind<Repository<Rule>>().To<EFRuleRepository>().InRequestScope();
            Bind<Repository<RuleTemplate>>().To<EFRuleTemplateRepository>().InRequestScope();
            Bind<Repository<SupportCategory>>().To<EFSupportCategoryRepository>().InRequestScope();
            Bind<Repository<TemplatedRule>>().To<EFTemplatedRuleRepository>().InRequestScope();
            Bind<Repository<UnvalidatedMessage>>().To<EFUnvalidatedMessageRepository>().InRequestScope();
            Bind<Repository<UnvalidatedMessageParameter>>().To<EFUnvalidatedMessageParameterRepository>().InRequestScope();
            Bind<Repository<AlertGroup>>().To<EFAlertGroupRepository>().InRequestScope();
            Bind<Repository<DefaultNote>>().To<EFDefaultNoteRepositroy>().InRequestScope();
        }
    }
}