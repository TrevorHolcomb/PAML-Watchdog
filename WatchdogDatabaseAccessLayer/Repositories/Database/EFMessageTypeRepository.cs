using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFMessageTypeRepository : Repository<MessageType>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFMessageTypeRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override IEnumerable<MessageType> Get()
        {
            return _container.MessageTypes.ToList();
        }

        public override MessageType GetByName(string name)
        {
            return _container.MessageTypes.Find(name);
        }

        public override void Insert(MessageType model)
        {
            _container.MessageTypes.Add(model);
        }

        public override void Delete(MessageType model)
        {
            _container.MessageTypes.Remove(model);
        }

        public override void Update(MessageType model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public override void Save()
        {
            _container.SaveChanges();
        }

        public override void Dispose()
        {
            
        }
    }
}
