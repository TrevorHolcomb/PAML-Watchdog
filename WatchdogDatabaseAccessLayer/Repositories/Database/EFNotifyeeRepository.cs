using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFNotifyeeRepository : IRepository<Notifyee>
    {
        private readonly WatchdogDatabaseContainer _container;
        public EFNotifyeeRepository()
        {
            _container = new WatchdogDatabaseContainer();
        }

        public IEnumerable<Notifyee> Get()
        {
            return _container.Notifyees.ToList();
        }

        public Notifyee GetById(int id)
        {
            return _container.Notifyees.Find(id);
        }

        public void Insert(Notifyee model)
        {
            _container.Notifyees.Add(model);
        }

        public void Delete(Notifyee model)
        {
            _container.Notifyees.Remove(model);
        }

        public void Update(Notifyee model)
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
