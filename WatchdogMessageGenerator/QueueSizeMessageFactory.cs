using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer;
using WatchdogMessageGenerator.MessageTypes;

namespace WatchdogMessageGenerator
{
    public class QueueSizeMessageFactory : AbstractMessageFactory
    {
        public const int MaxSize = 100000;
        public QueueSizeMessageFactory(string[] servers, string[] origins) : base(servers, origins, new QueueSizeMessageType())
        {
            
        }

        public int GetRandomQueueSize()
        {
            return Random.Next(MaxSize);
        }


        public override Message Build()
        {
            return new Message
            {
                Id = GetRandomId(),
                MessageTypeId = MessageType.Id,
                Origin = GetRandomOrigin(),
                Params = GetParams(),
                Processed = false,
                Server = GetRandomServer()
            };
        }

        public override string GetParams()
        {
            return "size=" + GetRandomQueueSize();
        }
    }


}
