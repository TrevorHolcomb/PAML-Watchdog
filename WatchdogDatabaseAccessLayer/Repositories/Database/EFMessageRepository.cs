using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFMessageRepository : IRepository<Message>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFMessageRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public IEnumerable<Message> Get()
        {
            return _container.Messages.ToList();
        }

        public Message GetById(int id)
        {
            return _container.Messages.Find(id);
        }

        public void Insert(Message model)
        {
            _container.Messages.Add(model);
        }

        public void Delete(Message model)
        {
            _container.Messages.Remove(model);
        }

        public void Update(Message model)
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
