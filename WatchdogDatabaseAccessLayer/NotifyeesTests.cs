using System;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using Xunit;

namespace WatchdogDatabaseAccessLayer
{
    public class NotifyeesTests
    {


        [Fact]
        public void TestAddNotifyeesAndGroups()
        {
            using (var db = new WatchdogDatabaseContainer())
            {
                db.NotifyeeGroups.ToList().ForEach(e => e.Notifyees.Clear());
                db.SaveChanges();
                db.NotifyeeGroups.RemoveRange(db.NotifyeeGroups.ToList());
                db.Notifyees.RemoveRange(db.Notifyees.ToList());
                db.SaveChanges();

                var Person1 = new Notifyee
                {
                    CellPhoneNumber = "0",
                    Email = "0",
                    Name = "Person 1"
                };

                var Person2 = new Notifyee
                {
                    CellPhoneNumber = "0",
                    Email = "0",
                    Name = "Person 2"
                };

                var GroupA = new NotifyeeGroup
                {
                    Name = "Group A",
                    Description = "null"
                };

                GroupA.Notifyees.Add(Person1);
                GroupA.Notifyees.Add(Person2);

                var GroupB = new NotifyeeGroup
                {
                    Name = "Group B",
                    Description = "null"
                };

                GroupB.Notifyees.Add(Person1);

                db.Notifyees.Add(Person1);
                db.Notifyees.Add(Person2);
                db.SaveChanges();

                db.NotifyeeGroups.Add(GroupA);
                db.NotifyeeGroups.Add(GroupB);
                db.SaveChanges();
            }

        }
    }
}
