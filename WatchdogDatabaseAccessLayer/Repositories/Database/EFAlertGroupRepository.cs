using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFAlertGroupRepository : Repository<AlertGroup>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFAlertGroupRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override void Dispose()
        {

        }

        public override IEnumerable<AlertGroup> Get()
        {
            return _container.AlertGroups.ToList();
        }

        public override AlertGroup GetById(int id)
        {
            return _container.AlertGroups.Find(id);
        }

        public override void Insert(AlertGroup model)
        {
            _container.AlertGroups.Add(model);
        }

        public override void Delete(AlertGroup model)
        {
            _container.AlertGroups.Remove(model);
        }

        public override void Update(AlertGroup model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public override void Save()
        {
            _container.SaveChanges();
        }
    }
}

