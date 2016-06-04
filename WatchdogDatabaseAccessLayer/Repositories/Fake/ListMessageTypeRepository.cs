using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListMessageTypeRepository : Repository<MessageType>
    {
        private readonly List<MessageType> _messageTypes;

        public ListMessageTypeRepository()
        {
            _messageTypes = new List<MessageType>();
        }
        public override void Dispose()
        {
            //do nothing
        }

        public override IEnumerable<MessageType> Get()
        {
            return _messageTypes.ToList();
        }

        public override MessageType GetByName(string name)
        {
            return _messageTypes.Find(type => type.Name == name);
        }

        public override void Insert(MessageType model)
        {
            _messageTypes.Add(model);
        }

        public override void Delete(MessageType model)
        {
            _messageTypes.Remove(model);
        }

        public override void Update(MessageType model)
        {
            //do nothing
        }

        public override void Save()
        {
            //do nothing
        }
    }
}
