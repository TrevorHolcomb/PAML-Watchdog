using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer;

namespace WatchdogMessageGenerator
{
    public class QueueSizeMessageFactory : AbstractMessageFactory
    {
        public const int MaxSize = 100000;
        public QueueSizeMessageFactory(string[] servers, string[] origins, int messageTypeId) : base(servers, origins, messageTypeId)
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
                MessageTypeId = MessageTypeId,
                Params = GetParams(),
                IsProcessed = false
            };
        }

        //TODO: update to latest message standard
        public override string GetParams()
        {
            return @"{
                ""messageTypeId"": " + 
                $"'origin':'{GetRandomOrigin()}','server':'{GetRandomServer()}', 'queueSize':'{GetRandomQueueSize()}'" + "}";
        }
    }


}