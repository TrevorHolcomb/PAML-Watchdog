using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WatchdogMessageGenerator.Tests
{
    public class QueueSizeMessageFactoryTests
    {
        [Theory]
        [MemberData(nameof(TestBuildData))]
        public void TestBuild(string[] origins, string[] servers)
        {
            var factory = new QueueSizeMessageFactory(servers, origins, 0);
            for (var i = 0; i < 10; i++)
            {
                var message = factory.Build();

                Assert.Contains(message.Origin, origins);
                Assert.Contains(message.Server, servers);

                var size = int.Parse(message.Params.Replace("size=", ""));
                Assert.InRange(size, 0, QueueSizeMessageFactory.MaxSize);
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
