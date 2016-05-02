using Manatee.Json.Schema;

namespace WatchdogDatabaseAccessLayer
{
    //A centralized class for holding the schemas to be used by RuleEngine, WebAPI, etc
    public static class Schemata
    {
        public static readonly IJsonSchema MessageSchema = JsonSchemaFactory.Load("Schemata\\MessageSchema.json");
        public static readonly IJsonSchema QuerySizeMessageSchema = JsonSchemaFactory.Load("Schemata\\QueueSizeMessageSchema.json");
    }
}
