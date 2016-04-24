using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Watchdog;
using WatchdogDatabaseAccessLayer;
using Xunit;
using Xunit.Extensions;

namespace Watchdog.Tests
{


    public class PollingWatchdogTests
    {
        [Theory]
        [MemberData("MessageTestData")]
        public void TestWatch(List<Message> messages)
        {

            //Arrange
            WatchdogDatabaseContext dbContext = new WatchdogDatabaseContext();
            //clear out old entries
            var allMessages = dbContext.Messages.Select(e => e);
            dbContext.Messages.RemoveRange(allMessages);
            dbContext.SaveChanges();
            //add new ones
            dbContext.Messages.AddRange(messages);
            dbContext.SaveChanges();          

            //Act
            AbstractWatchdog watchdog = new PollingWatchdog().GetInstance();
            watchdog.Watch();
            Thread.Sleep(100);  //there's probably a better way of avoiding race conditions

            //Assert
            Assert.Empty(dbContext.Messages);

            //Cleanup
            watchdog.StopWatching();
        }

        [Theory]
        [MemberData("MessageTestData")]
        public void TestStopWatching(List<Message> messages)
        {
            //Arrange
            WatchdogDatabaseContext dbContext = new WatchdogDatabaseContext();
            //clear out old entries
            var allMessages = dbContext.Messages.Select(e => e);
            dbContext.Messages.RemoveRange(allMessages);
            dbContext.SaveChanges();
            //add new ones
            dbContext.Messages.AddRange(messages);
            dbContext.SaveChanges();

            //Act
            AbstractWatchdog watchdog = new PollingWatchdog().GetInstance();
            watchdog.Watch();
            watchdog.StopWatching();

            Thread.Sleep(100);
            dbContext.Messages.AddRange(messages);
            dbContext.SaveChanges();

            //Assert
            Assert.NotEmpty(dbContext.Messages.ToList());
            Assert.Equal(messages.Count, dbContext.Messages.ToList().Count);
        }

        public static IEnumerable<object[]> MessageTestData
        {
            get
            {
                yield return new object[] { new List<Message> {
                    new Message
                    {
                        Id = 0,
                        MessageTypeId = 0,
                        Origin = "origin",
                        Params = "size=1",
                        Processed = false,
                        Server = "server"
                    }
                } };
                
                yield return new object[] { new List<Message> {
                    new Message
                    {
                        Id = 1,
                        MessageTypeId = 0,
                        Origin = "Athens",
                        Params = "size=1",
                        Processed = false,
                        Server = "Homer"
                    },

                    new Message
                    {
                        Id = 2,
                        MessageTypeId = 0,
                        Origin = "Delphi",
                        Params = "size=3",
                        Processed = false,
                        Server = "TheOracle"
                    },

                    new Message
                    {
                        Id = 3,
                        MessageTypeId = 0,
                        Origin = "Macedonia",
                        Params = "size=5",
                        Processed = false,
                        Server = "Alexander"
                    }
                } };
            }
        }
    }
}
