using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;

namespace WatchdogDatabaseAccessLayer
{
    public class WatchdogDatabaseContextMocker
    {
        public static WatchdogDatabaseContext Mock(IEnumerable<Rule> rules,
            IEnumerable<Message> messages)
        {

            var mockRules = DbSetMocker.Mock<Rule>(rules);
            var mockMessages = DbSetMocker.Mock<Message>(messages);
            var mockAlerts = DbSetMocker.Mock<Alert>(new List<Alert>());

            var mockedDbContext = new Mock<WatchdogDatabaseContext>();

            mockedDbContext.Setup(e => e.Messages).Returns(mockMessages.Object);
            mockedDbContext.Setup(e => e.Rules).Returns(mockRules.Object);
            mockedDbContext.Setup(e => e.Alerts).Returns(mockAlerts.Object);

            return mockedDbContext.Object;
        }



        private static class DbSetMocker
        {
            public static Mock<DbSet<T>> Mock<T>(IEnumerable<T> collection) where T : class
            {
                var data = collection.AsQueryable();

                var mockSet = new Mock<DbSet<T>>();
                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
                mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

                mockSet.Setup(e => e.Add(It.IsAny<T>())).Callback<T>(collection.ToList().Add);
                mockSet.Setup(e => e.Add(It.IsAny<T>())).Returns<T>(e=>e);
                
                
                return mockSet;
            }
        }
    }
}
