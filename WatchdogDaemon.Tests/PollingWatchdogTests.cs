using System;
using System.Collections.Generic;
using System.Linq;
using WatchdogDatabaseAccessLayer;
using WatchdogMessageGenerator;
using Xunit;

namespace WatchdogDaemon.Tests
{
    
    public class PollingWatchdogTests
    {
        [Fact]
        public void Watch()
        {
            var messages = new List<Message>();
            var factory = new QueueSizeMessageFactory(new string[] {"Server-1"}, new string[] {"Webinterface-1"}, 0);
            for(var i = 0; i < 100; i++)
                messages.Add(factory.Build());

            var watchdog = new PollingWatchdog(
                WatchdogDatabaseContextMocker.Mock(
                    new List<Rule>(), 
                    messages), 
                new RuleEngine());

            watchdog.Watch();

            while (watchdog.DbContext.Messages.Count(e => !e.IsProcessed) != 0)
            {
                // busy wait loop cause why not.
            }

            watchdog.StopWatching();
        }
    }
}
