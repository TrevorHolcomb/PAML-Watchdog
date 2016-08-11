using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDatabaseAccessLayer.Models
{
    public partial class MessageParameter
    {
        public string Name => this.MessageTypeParameterType.Name;
    }
}
