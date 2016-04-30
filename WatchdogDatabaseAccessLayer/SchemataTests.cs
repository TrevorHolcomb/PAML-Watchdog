using System;
using Xunit;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Json.Schema;

namespace WatchdogDatabaseAccessLayer
{
    public class SchemataTests
    {
        public static TheoryData<string, bool> data = new TheoryData<string, bool>
        {
            {
                @"{
                    ""Server"":""testserver"",
                    ""MessageTypeId"":0,
                    ""Params"":""testParam""
                }",
                true
            },
            {
                @"{
                    ""asdfasdf"":""no bueno"",
                    ""MessageTypeId"":0,
                    ""Params"":""testParam""
                }",
                false
            }

        };
        //this test demonstrates two ways to create and use schema: programmatically, and through deserialization of a Json schema stored in a string
        [Theory]
        [MemberData(nameof(data))]
        public void TestMessageSchema(string data, bool isValidData)
        {
            //Arrange
            ObjectSchema programmaticallyCreatedSchema = new ObjectSchema
            {
                Properties = new JsonSchemaPropertyDefinitionCollection
                {
                    new JsonSchemaPropertyDefinition("server")
                    {
                        Type = new StringSchema { MaxLength = 128 },
                        IsRequired = true
                    },
                    new JsonSchemaPropertyDefinition("messageTypeId")
                    {
                        Type = new IntegerSchema() { Minimum = 0 },
                        IsRequired = true
                    },
                    new JsonSchemaPropertyDefinition("params")
                    {
                        Type = new ObjectSchema(),
                        IsRequired = true
                    }
                }
            };
            

            string ValidTestMessage =
                @"{
                    ""server"": ""testserver"",
                    ""origin"": ""testland"",
                    ""messageTypeId"": 0,
                    ""params"": {
                        ""testKey"": ""testValue""
                    }
                }";

            string InvalidTestMessage =
                @"{
                    ""asdfasdf"": ""no bueno"",
                    ""origin"": ""testland"",
                    ""messageTypeId"": 0,
                    ""params"": {
                        ""testKey"": ""testValue""
                    }
                }";

            JsonValue ValidTestMessageJson = JsonValue.Parse(ValidTestMessage);
            JsonValue InvalidTestMessageJson = JsonValue.Parse(InvalidTestMessage);

            IJsonSchema DeserializedFromJsonSchema = Schemata.MessageSchema;

            JsonValue validTestMessageJson = JsonValue.Parse(data);
            IJsonSchema deserializedFromJsonSchema = Schemata.MessageSchema;

            //Act
            bool isValid = programmaticallyCreatedSchema.Validate(validTestMessageJson).Valid;
            bool isValid2 = deserializedFromJsonSchema.Validate(validTestMessageJson).Valid;

            if (isValidData)
            {
                Assert.True(isValid);
                Assert.True(isValid2);
            }
            else
            {
                Assert.False(isValid2);
                Assert.False(isValid2);
            }
        }

        [Fact]
        public void TestQueueSizeMessageSchema()
        {
            //Arrange
            string ValidQueueSizeMessageJson = @"{
                    ""server"":""Some Server"",
                    ""origin"":""Main System"",
                    ""messageTypeId"":0,
                    ""params"":{
                        ""queueSize"":100
                    }
                }";

            string InvalidQueueSizeMessageJson = @"{
                    ""server"":""Some Server"",
                    ""origin"":""Main System"",
                    ""messageTypeId"":2,
                    ""params"":{
                        ""queueSize"":100
                    }
                }";

            JsonValue ValidQueueSizeMessage = JsonValue.Parse(ValidQueueSizeMessageJson);
            JsonValue InvalidQueueSizeMessage = JsonValue.Parse(InvalidQueueSizeMessageJson);

            //Act
            Boolean isValid = Schemata.MessageSchema.Validate(ValidQueueSizeMessage).Valid;
            isValid &= Schemata.QuerySizeMessageSchema.Validate(ValidQueueSizeMessage).Valid;

            Boolean isInvalid = Schemata.MessageSchema.Validate(InvalidQueueSizeMessage).Valid;
            isInvalid &= Schemata.QuerySizeMessageSchema.Validate(InvalidQueueSizeMessageJson).Valid;

            double? messageType = JsonValue.Parse(ValidQueueSizeMessageJson).Object.TryGetNumber("messageTypeId");
            
            //Assert
            Assert.True(isValid);
            Assert.False(isInvalid);
        }
    }
}