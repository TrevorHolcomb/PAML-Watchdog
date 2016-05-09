namespace WatchdogDatabaseAccessLayer.Models
{
    public partial class EscalationChainLink
    {
        public override bool Equals(object obj)
        {
            var link = obj as EscalationChainLink;
            return link?.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();  
        }
    }
}
