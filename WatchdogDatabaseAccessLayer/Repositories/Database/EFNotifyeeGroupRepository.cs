using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFNotifyeeGroupRepository : IRepository<NotifyeeGroup>
    {
        private readonly WatchdogDatabaseContainer _container;
        public EFNotifyeeGroupRepository()
        {
            _container = new WatchdogDatabaseContainer();
        }

        public IEnumerable<NotifyeeGroup> Get()
        {
            return _container.NotifyeeGroups.ToList();
        }

        public NotifyeeGroup GetById(int id)
        {
            return _container.NotifyeeGroups.Find(id);
        }

        public void Insert(NotifyeeGroup model)
        {
            _container.NotifyeeGroups.Add(model);
        }

        public void Delete(NotifyeeGroup model)
        {
            _container.NotifyeeGroups.Remove(model);
        }

        public void Update(NotifyeeGroup model)
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
