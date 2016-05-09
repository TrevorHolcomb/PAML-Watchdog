using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListMessageRepository : IRepository<Message>
    {
        private readonly List<Message> _messages;

        public ListMessageRepository()
        {
            _messages = new List<Message>();
        }
        public void Dispose()
        {
            //do nothing
        }

        public IEnumerable<Message> Get()
        {
            return _messages.ToList();
        }

        public Message GetById(int id)
        {
            return _messages.Find(message => message.Id == id);
        }

        public void Insert(Message model)
        {
            _messages.Add(model);
        }

        public void Delete(Message model)
        {
            _messages.Remove(model);
        }

        public void Update(Message model)
        {
            //do nothing
        }

        public void Save()
        {
            //do nothing
        }
    }
}
