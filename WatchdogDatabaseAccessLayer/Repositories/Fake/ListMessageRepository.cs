using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListMessageRepository : Repository<Message>
    {
        private readonly List<Message> _messages;

        public ListMessageRepository()
        {
            _messages = new List<Message>();
        }
        public override void Dispose()
        {
            //do nothing
        }

        public override IEnumerable<Message> Get()
        {
            return _messages.ToList();
        }

        public override Message GetById(int id)
        {
            return _messages.Find(message => message.Id == id);
        }

        public override void Insert(Message model)
        {
            _messages.Add(model);
        }

        public override void Delete(Message model)
        {
            _messages.Remove(model);
        }

        public override void Update(Message model)
        {
            //do nothing
        }

        public override void Save()
        {
            //do nothing
        }
    }
}
