using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogMessageGenerator
{
    public abstract class AbstractMessageFactory
    {
        public Engine Engine { get; }
        public string[] Servers { get; }
        public string[] Origins { get; }
        public MessageType MessageType { get; set; }

        internal readonly Random Random;

        protected AbstractMessageFactory(Engine engine, string[] servers, string[] origins, MessageType messageType)
        {
            Engine = engine;
            Servers = servers;
            Origins = origins;
            MessageType = messageType;

            Random = new Random();
        }

        public int GetRandomId()
        {
            return Random.Next();
        }

        public string GetRandomServer()
        {
            return GetRandomStringFromArray(Servers);
        }

        public string GetRandomOrigin()
        {
            return GetRandomStringFromArray(Origins);
        }

        private string GetRandomStringFromArray(IReadOnlyList<string> array)
        {
            return array[Random.Next(array.Count)];
        }

        public abstract Message Build();
    }

    
}