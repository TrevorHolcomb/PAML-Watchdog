using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;
using Xunit;

namespace WatchdogMessageGenerator.Tests
{
    public class QueueSizeMessageFactoryTests
    {
        [Theory]
        [MemberData(nameof(TestBuildData))]
        public void TestBuild(string[] origins, string[] servers)
        {
            MessageType queueSizeMessageType = new MessageType() { Id = 0, Name = "queueSizeMessage", Description = "a message indicating the size of some queue" };
            var factory = new QueueSizeMessageFactory(servers, origins, queueSizeMessageType);
            for (var i = 0; i < 10; i++)
            {
                var message = factory.Build();

                Assert.Contains(message.Origin, origins);
                Assert.Contains(message.Server, servers);
            }
        }

        public static TheoryData<string[], string[]> TestBuildData => new TheoryData<string[], string[]>()
        {
            {
                new string[] {"origin-1","origin-2","origin-3"}, new string[] {"server-1","server-2"}
            },
            {
                new string[] {"origin-1"}, new string[] {"server-1"} 
            }
        };
    }
}
