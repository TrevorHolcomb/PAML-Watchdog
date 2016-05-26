using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFNotifyeeGroupRepository : Repository<NotifyeeGroup>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFNotifyeeGroupRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override IEnumerable<NotifyeeGroup> Get()
        {
            return _container.NotifyeeGroups.ToList();
        }

        public override NotifyeeGroup GetById(int id)
        {
            return _container.NotifyeeGroups.Find(id);
        }

        public override NotifyeeGroup GetByName(string name)
        {
            return _container.NotifyeeGroups.FirstOrDefault(group => group.Name == name);
        }

        public override void Insert(NotifyeeGroup model)
        {
            _container.NotifyeeGroups.Add(model);
        }

        public override void Delete(NotifyeeGroup model)
        {
            _container.NotifyeeGroups.Remove(model);
        }

        public override void Update(NotifyeeGroup model)
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
