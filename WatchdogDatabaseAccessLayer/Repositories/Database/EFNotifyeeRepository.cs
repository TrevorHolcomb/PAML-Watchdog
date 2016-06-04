using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFNotifyeeRepository : Repository<Notifyee>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFNotifyeeRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override IEnumerable<Notifyee> Get()
        {
            return _container.Notifyees.ToList();
        }

        public override Notifyee GetById(int id)
        {
            return _container.Notifyees.Find(id);
        }

        public override Notifyee GetByName(string name)
        {
            return _container.Notifyees.Where(notifyee => notifyee.Name == name).DefaultIfEmpty(null).First();
        }

        public override void Insert(Notifyee model)
        {
            _container.Notifyees.Add(model);
        }

        public override void Delete(Notifyee model)
        {
            _container.Notifyees.Remove(model);
        }

        public override void Update(Notifyee model)
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
