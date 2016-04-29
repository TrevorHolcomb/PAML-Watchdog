using Manatee.Json;
using Manatee.Json.Schema;
using Manatee.Json.Serialization;

namespace WatchdogDatabaseAccessLayer
{
    //A centralized class for holding the schemas to be used by RuleEngine, WebAPI, etc
    public static class Schemata
    {
        private static readonly string _MessageSchemaJson =
            @"{ 
                ""title"": ""Message Schema"",
                ""type"": ""object"",
                ""description"": ""this schema is used to validate messages"",
                ""properties"":{
                    ""Server"":{
                        ""type"":""string"",
                        ""maxLength"":128
                    },
                    ""MessageTypeId"":{
                        ""type"":""integer"",
                        ""minimum"": 0
                    },
                    ""Params"":{
                        ""type"":""string"",
                        ""maxLength"":512
                    }
                },
                ""required"":[""Server"", ""MessageTypeId"", ""Params""]
            }";

        public static readonly IJsonSchema MessageSchema = JsonSchemaFactory.FromJson(JsonValue.Parse(_MessageSchemaJson));

    }
}
