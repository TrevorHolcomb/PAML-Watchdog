using System.Collections.Generic;
using System.IO;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer
{
    public class MessageParameterFactory
    {
        public class RawMessageParameter
        {
            internal string Type { get; set; }
            internal string Value { get; set; }
        }

        public static RawMessageParameter WrapParameter(string type, string value)
        {
            return new RawMessageParameter
            {
                Type = type,
                Value = value
            };
        }

        public static MessageParameter BuildParameter(RawMessageParameter rawParameter, MessageTypeParameterType parameterType)
        {
            if (rawParameter.Type != parameterType.Type)
            {
                throw new InvalidDataException();
            }

            return new MessageParameter
            {
                MessageTypeParameterType = parameterType,
                Value = rawParameter.Value,
                MessageTypeColumnId = parameterType.Id
            };
        }

        public static ICollection<MessageParameter> BuildParameters(Message message, Dictionary<string, RawMessageParameter> rawMessageParameters)
        {
            var messageParameters = new List<MessageParameter>();
            
            foreach (var parameterType in message.MessageType.MessageTypeParameterTypes)
            {
                var rawParameter = rawMessageParameters[parameterType.Name];
                var messageParameter = BuildParameter(rawParameter, parameterType);
                messageParameter.Message = message;
                messageParameter.MessageId = message.Id;
                messageParameters.Add(messageParameter);
            }

            return messageParameters;
        }
    }
}