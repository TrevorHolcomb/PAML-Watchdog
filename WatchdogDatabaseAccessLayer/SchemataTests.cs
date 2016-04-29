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
                    new JsonSchemaPropertyDefinition("Server")
                    {
                        Type = new StringSchema { MaxLength = 128 },
                        IsRequired = true
                    },
                    new JsonSchemaPropertyDefinition("MessageTypeId")
                    {
                        Type = new IntegerSchema() { Minimum = 0 },
                        IsRequired = true
                    },
                    new JsonSchemaPropertyDefinition("Params")
                    {
                        Type = new StringSchema() { MaxLength = 512 },
                        IsRequired = true
                    }
                }
            };
            

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
    }
}