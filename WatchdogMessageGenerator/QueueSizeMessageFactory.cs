﻿using System.Collections.Generic;
using WatchdogDatabaseAccessLayer;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogMessageGenerator
{
    public class QueueSizeMessageFactory : AbstractMessageFactory
    {
        public const int MaxSize = 100000;
        public QueueSizeMessageFactory(string[] servers, string[] origins, MessageType messageType) : base(servers, origins, messageType)
        {
            
        }

        public int GetRandomQueueSize()
        {
            return Random.Next(MaxSize);
        }


        public override Message Build()
        {
            var message = new Message
            {
                Id = GetRandomId(),
                MessageTypeName = MessageType.Name,
                MessageType = MessageType,
                IsProcessed = false,
                Origin = GetRandomOrigin(),
                Server = GetRandomServer(),
            };

            var dictionary = new Dictionary<string, MessageParameterFactory.RawMessageParameter>
            {
                {MessageType.Name, MessageParameterFactory.WrapParameter("integer", GetRandomQueueSize().ToString())}
            };

            message.MessageParameters = MessageParameterFactory.BuildParameters(message, dictionary);

            return message;
        }
    }
}