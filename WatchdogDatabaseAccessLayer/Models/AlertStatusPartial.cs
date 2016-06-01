using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDatabaseAccessLayer.Models
{
    public partial class AlertStatus
    {
        public AlertStatus MostRecent()
        {
            var recent = this;
            while (recent.Next != null)
                recent = recent.Next;

            return recent;
        }

        public AlertStatus FirstStatus()
        {
            var first = this;
            while (first.Prev != null)
                first = first.Prev;

            return first;
        }
    }
}
