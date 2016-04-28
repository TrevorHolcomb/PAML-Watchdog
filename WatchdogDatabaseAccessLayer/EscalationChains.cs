using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WatchdogDatabaseAccessLayer
{
    public class EscalationChains
    {
        [Fact]
        public void TestAddChains()
        {
            using (var db = new WatchdogDatabaseContainer())
            {
                Reset(db);

                var group = db.NotifyeeGroups.Add(new NotifyeeGroup
                {
                    Name = "Group",
                    Description = "A group"
                });
                db.SaveChanges();

                var root = new EscalationChainLink
                {
                    NotifyeeGroup = group
                };
                db.SaveChanges();

                db.EscalationChains.Add(new EscalationChain
                {
                    Name = "Standard Chain",
                    Description = "a standard escalation chain",
                    EscalationChainRootLink = root
                });
                db.SaveChanges();

                Reset(db);
            }
        }

        private static void Reset(WatchdogDatabaseContainer db)
        {
            db.EscalationChains.ToList().ForEach(e =>
            {
                var escalationChain = e;
                var links = new List<EscalationChainLink>();
                var link = escalationChain.EscalationChainRootLink;
                links.Add(link);
                for (; link != null; link = link.NextLink)
                    links.Add(link);

                db.EscalationChains.Remove(escalationChain);
                db.EscalationChainLinks.RemoveRange(links);
            });
            db.NotifyeeGroups.RemoveRange(db.NotifyeeGroups.ToList());
            db.SaveChanges();
        }
    }
}
