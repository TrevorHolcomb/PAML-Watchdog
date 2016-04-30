﻿using System;
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