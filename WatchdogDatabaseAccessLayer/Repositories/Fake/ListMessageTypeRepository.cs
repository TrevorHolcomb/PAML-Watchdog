using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListMessageTypeRepository : IMessageTypeRepository
    {
        private readonly List<MessageType> _messageTypes;

        public ListMessageTypeRepository()
        {
            _messageTypes = new List<MessageType>();
        }
        public void Dispose()
        {
            //do nothing
        }

        public IEnumerable<MessageType> Get()
        {
            return _messageTypes.ToList();
        }

        public MessageType GetById(int id)
        {
            return _messageTypes.Find(MessageType => MessageType.Id == id);
        }

        public void Insert(MessageType model)
        {
            _messageTypes.Add(model);
        }

        public void Delete(MessageType model)
        {
            _messageTypes.Remove(model);
        }

        public void Update(MessageType model)
        {
            //do nothing
        }

        public void Save()
        {
            //do nothing
        }
    }
}
