using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDatabaseAccessLayer
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
