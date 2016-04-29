using System;
using Xunit;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Json.Schema;

namespace WatchdogDatabaseAccessLayer
{
    public class SchemataTests
    {
        //this test demonstrates two ways to create and use schema: programmatically, and through deserialization of a Json schema stored in a string
        [Fact]
        public void TestMessageSchema()
        {
            //Arrange
            ObjectSchema ProgrammaticallyCreatedSchema = new ObjectSchema
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

            string ValidTestMessage =
                @"{
                    ""Server"":""testserver"",
                    ""MessageTypeId"":0,
                    ""Params"":""testParam""
                }";

            string InvalidTestMessage =
                @"{
                    ""asdfasdf"":""no bueno"",
                    ""MessageTypeId"":0,
                    ""Params"":""testParam""
                }";

            JsonValue ValidTestMessageJson = JsonValue.Parse(ValidTestMessage);
            JsonValue InvalidTestMessageJson = JsonValue.Parse(InvalidTestMessage);

            IJsonSchema DeserializedFromJsonSchema = Schemata.MessageSchema;


            //Act
            bool isValid = ProgrammaticallyCreatedSchema.Validate(ValidTestMessageJson).Valid;
            bool isInvalid = ProgrammaticallyCreatedSchema.Validate(InvalidTestMessageJson).Valid;

            bool isValid2 = DeserializedFromJsonSchema.Validate(ValidTestMessageJson).Valid;
            bool isInvalid2 = DeserializedFromJsonSchema.Validate(InvalidTestMessageJson).Valid;


            //Assert
            Assert.True(isValid);
            Assert.False(isInvalid);
            Assert.True(isValid2);
            Assert.False(isInvalid2);
        }
    }
}