using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFMessageRepository : Repository<Message>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFMessageRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override IEnumerable<Message> Get()
        {
            return _container.Messages.ToList();
        }

        public override Message GetById(int id)
        {
            return _container.Messages.Find(id);
        }

        public override void Insert(Message model)
        {
            _container.Messages.Add(model);
        }

        public override void Delete(Message model)
        {
            _container.Messages.Remove(model);
        }

        public override void Update(Message model)
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
