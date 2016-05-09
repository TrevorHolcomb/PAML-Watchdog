using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListMessageTypeParameterTypeRepository : IRepository<MessageTypeParameterType>
    {
        private readonly List<MessageTypeParameterType> _messageTypeParameterTypes;

        public ListMessageTypeParameterTypeRepository()
        {
            _messageTypeParameterTypes = new List<MessageTypeParameterType>();
        }
        public void Dispose()
        {
            //do nothing
        }

        public IEnumerable<MessageTypeParameterType> Get()
        {
            return _messageTypeParameterTypes.ToList();
        }

        public MessageTypeParameterType GetById(int id)
        {
            return _messageTypeParameterTypes.Find(messageTypeParameterType => messageTypeParameterType.Id == id);
        }

        public void Insert(MessageTypeParameterType model)
        {
            _messageTypeParameterTypes.Add(model);
        }

        public void Delete(MessageTypeParameterType model)
        {
            _messageTypeParameterTypes.Remove(model);
        }

        public void Update(MessageTypeParameterType model)
        {
            //do nothing
        }

        public void Save()
        {
            //do nothing
        }
    }
}
