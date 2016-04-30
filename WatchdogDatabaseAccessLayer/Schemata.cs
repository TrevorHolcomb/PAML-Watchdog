using Manatee.Json;
using Manatee.Json.Schema;
using Manatee.Json.Serialization;

namespace WatchdogDatabaseAccessLayer
{
    //A centralized class for holding the schemas to be used by RuleEngine, WebAPI, etc
    public static class Schemata
    {
        public static readonly IJsonSchema MessageSchema = JsonSchemaFactory.Load("MessageSchema.json");
        public static readonly IJsonSchema QuerySizeMessageSchema = JsonSchemaFactory.Load("MessageTypeSchemata\\QueueSizeMessageSchema.json");
    }
}
