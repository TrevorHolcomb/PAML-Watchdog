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
                    
                    ""messageTypeId"":0,
                    ""params"":{
                        ""server"":""Some Server"",
                        ""origin"":""Main System"",
                        ""queueSize"":100
                    }
                }",
                true
            },
            {
                @"{
                    ""messageTypeId"":0,
                    ""params"":{
                        ""asdfasdf"":""Some Server"",
                        ""origin"":""Main System"",
                        ""queueSize"":100
                    }
                }",
                false
            }
        };

        public static TheoryData<string, bool> queueSizeData = new TheoryData<string, bool>
        {
            {
                @"{
                    ""messageTypeId"":2,
                    ""params"":{
                        ""server"":""Some Server"",
                        ""origin"":""Main System"",
                        ""queueSize"":100
                    }
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
                    new JsonSchemaPropertyDefinition("messageTypeId")
                    {
                        Type = new IntegerSchema() { Minimum = 0 },
                        IsRequired = true
                    },
                    new JsonSchemaPropertyDefinition("params")
                    {
                        Type = new ObjectSchema() {
                            Properties = new JsonSchemaPropertyDefinitionCollection
                            {
                                new JsonSchemaPropertyDefinition("server")
                                {
                                    Type = new StringSchema { MaxLength = 128 },
                                    IsRequired = true
                                },
                                new JsonSchemaPropertyDefinition("origin")
                                {
                                    Type = new StringSchema { MaxLength = 128 },
                                    IsRequired = true
                                }
                            }
                        }
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
                Assert.False(isValid);
                Assert.False(isValid2);
            }
        }

        [Theory]
        [MemberData(nameof(data))]
        [MemberData(nameof(queueSizeData))]
        public void TestQueueSizeMessageSchema(string data, bool isValidData)
        {
            //Arrange

            JsonValue ValidQueueSizeMessage = JsonValue.Parse(data);

            //Act
            Boolean isValid = Schemata.MessageSchema.Validate(ValidQueueSizeMessage).Valid;
            isValid &= Schemata.QuerySizeMessageSchema.Validate(ValidQueueSizeMessage).Valid;

            //Assert
            if (isValidData)
                Assert.True(isValid);
            else
                Assert.False(isValid);
        }
    }
}