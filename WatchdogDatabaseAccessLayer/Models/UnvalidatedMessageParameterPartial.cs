using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDatabaseAccessLayer.Models
{
    public partial class UnvalidatedMessageParameter
    {
        public MessageParameter ToMessageParameter(MessageTypeParameterType parameterType)
        {
            return new MessageParameter
            {
                Value = this.Value,
                MessageTypeParameterType = parameterType,
                MessageTypeParameterTypeId = parameterType.Id
            };
        }
    }
}
