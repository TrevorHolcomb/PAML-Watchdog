using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListMessageTypeParameterTypeRepository : Repository<MessageTypeParameterType>
    {
        private readonly List<MessageTypeParameterType> _messageTypeParameterTypes;

        public ListMessageTypeParameterTypeRepository()
        {
            _messageTypeParameterTypes = new List<MessageTypeParameterType>();
        }
        public override void Dispose()
        {
            //do nothing
        }

        public override IEnumerable<MessageTypeParameterType> Get()
        {
            return _messageTypeParameterTypes.ToList();
        }

        public override MessageTypeParameterType GetById(int id)
        {
            return _messageTypeParameterTypes.Find(messageTypeParameterType => messageTypeParameterType.Id == id);
        }

        public override void Insert(MessageTypeParameterType model)
        {
            _messageTypeParameterTypes.Add(model);
        }

        public override void Delete(MessageTypeParameterType model)
        {
            _messageTypeParameterTypes.Remove(model);
        }

        public override void Update(MessageTypeParameterType model)
        {
            //do nothing
        }

        public override void Save()
        {
            //do nothing
        }
    }
}
