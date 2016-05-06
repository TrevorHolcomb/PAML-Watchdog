using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using WatchdogDatabaseAccessLayer;

namespace WatchdogDatabaseAccessLayer
{
    public class WatchdogDatabaseContextMocker
    {
        public static WatchdogDatabaseContainer Mock(ICollection<Rule> rules,
            ICollection<Message> messages)
        {

            var mockRules = DbSetMocker.Mock<Rule>(rules);
            var mockMessages = DbSetMocker.Mock<Message>(messages);
            var mockAlerts = DbSetMocker.Mock<Alert>(new List<Alert>());

            var mockedDbContext = new Mock<WatchdogDatabaseContainer>();

            mockedDbContext.Setup(mock => mock.Messages).Returns(mockMessages.Object);
            mockedDbContext.Setup(mock => mock.Rules).Returns(mockRules.Object);
            mockedDbContext.Setup(mock => mock.Alerts).Returns(mockAlerts.Object);

            return mockedDbContext.Object;
        }

        private static class DbSetMocker
        {
            public static Mock<DbSet<T>> Mock<T>(ICollection<T> collection) where T : class
            {
                var data = collection.AsQueryable();

                var mockSet = new Mock<DbSet<T>>();
                mockSet.As<IQueryable<T>>().Setup(mock => mock.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(mock => mock.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(mock => mock.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(mock => mock.GetEnumerator()).Returns(() => data.GetEnumerator());

                mockSet.Setup(mock => mock.Add(It.IsAny<T>())).Returns<T>(element => element).Callback<T>(element => collection.Add(element));
                
                return mockSet;
            }
        }
    }
}