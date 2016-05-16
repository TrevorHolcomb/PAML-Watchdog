namespace AdministrationPortal.Controllers
{
    public class RuleCreateViewModel
    {
        public int AlertTypeId { get; set; }
        public int MessageTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int EscalationChainId { get; set; }
        public string RuleTriggerSchema { get; set; }
        public string Origin { get; set; }
        public string Server { get; set; }
    }
}