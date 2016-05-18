namespace AdministrationPortal.ViewModels
{
    public class RuleCreateViewModel
    {
        public int AlertTypeId { get; set; }
        public int MessageTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DefaultSeverity { get; set; }
        public int EscalationChainId { get; set; }
        public string Expression { get; set; }
        public string Origin { get; set; }
        public string Server { get; set; }
        public string Engine { get; set; }
        public string RuleCreator { get; set; }
    }
}