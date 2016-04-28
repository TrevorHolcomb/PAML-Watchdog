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

                //TODO: get origins and servers from message.RequiredParams
                //Assert.Contains(message.Params, origins);
                //Assert.Contains(message.Params, servers);
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
