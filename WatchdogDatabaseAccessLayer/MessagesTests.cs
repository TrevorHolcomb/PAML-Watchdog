using System.Linq;
using Xunit;

namespace WatchdogDatabaseAccessLayer
{
    public class MessagesTests
    {
        [Fact]
        public void TestAddMessages()
        {
            using (var db = new WatchdogDatabaseContainer())
            {
                Reset(db);

                var mt = db.MessageTypes.Add(new MessageType
                {
                    Name = "qsu-jms",
                    Description = "queue size update",
                    OptionalParameters = "[]",
                    RequiredParameters = "['size']"
                });
                db.SaveChanges();

                db.Messages.Add(new Message
                {
                    MessageTypeId = mt.Id,
                    IsProcessed = false,
                    Params = "{'size':0}"
                });
                db.SaveChanges();

                Reset(db);
            }
        }

        private static void Reset(WatchdogDatabaseContainer db)
        {
            db.MessageTypes.RemoveRange(db.MessageTypes.ToList());
            db.Messages.RemoveRange(db.Messages.ToList());
        }
    }
}