﻿using System;
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
        public string[] Servers { get; }
        public string[] Origins { get; }
        public int MessageTypeId { get; }

        internal readonly Random Random;

        protected AbstractMessageFactory(string[] servers, string[] origins, int messageTypeId)
        {
            Servers = servers;
            Origins = origins;
            MessageTypeId = messageTypeId;

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