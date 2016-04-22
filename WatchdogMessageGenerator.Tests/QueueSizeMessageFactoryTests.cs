using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WatchdogMessageGenerator.Tests
{
    public class QueueSizeMessageFactoryTests
    {
        [Theory, ClassData(typeof(TestBuildData))]
        public void TestBuild(string[] origins, string[] servers)
        {
            var factory = new QueueSizeMessageFactory(servers, origins);
            var message = factory.Build();

            Assert.Contains(message.Origin, origins);
            Assert.Contains(message.Server, servers);

            int size = int.Parse(message.Params.Replace("size=", ""));

            Assert.InRange(size,0, QueueSizeMessageFactory.MaxSize);
        }
    }

    public class TestBuildData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {new string[] {"origin-1","origin-2","origin-3"}, new string[] {"server-1","server-2","server-3"}},
            new object[] {new string[] {"origin-1"}, new string[] {"server-1"}}
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
