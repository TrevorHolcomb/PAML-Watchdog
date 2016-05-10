using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFMessageTypeRepository : IRepository<MessageType>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFMessageTypeRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public IEnumerable<MessageType> Get()
        {
            return _container.MessageTypes.ToList();
        }

        public MessageType GetById(int id)
        {
            return _container.MessageTypes.Single(messageType => messageType.Id == id);
        }

        public void Insert(MessageType model)
        {
            _container.MessageTypes.Add(model);
        }

        public void Delete(MessageType model)
        {
            _container.MessageTypes.Remove(model);
        }

        public void Update(MessageType model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public void Save()
        {
            _container.SaveChanges();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
