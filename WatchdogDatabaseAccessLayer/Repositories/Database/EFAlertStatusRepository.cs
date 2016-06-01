using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    class EFAlertStatusRepository : Repository<AlertStatus>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFAlertStatusRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override void Dispose()
        {

        }

        public override IEnumerable<AlertStatus> Get()
        {
            return _container.AlertStatuses.ToList();
        }

        public override AlertStatus GetById(int id)
        {
            return _container.AlertStatuses.Find(id);
        }

        public override void Insert(AlertStatus model)
        {
            _container.AlertStatuses.Add(model);
        }

        public override void Delete(AlertStatus model)
        {
            _container.AlertStatuses.Remove(model);
        }

        public override void Update(AlertStatus model)
        {
            _container.Entry(model).State = System.Data.Entity.EntityState.Modified;
        }

        public override void Save()
        {
            _container.SaveChanges();
        }
    }
}
