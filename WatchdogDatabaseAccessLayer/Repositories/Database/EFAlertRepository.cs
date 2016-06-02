using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFAlertRepository : Repository<Alert>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFAlertRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override void Dispose()
        {
            
        }

        public override IEnumerable<Alert> Get()
        {
            return _container.Alerts.ToList();
        }

        public override Alert GetById(int id)
        {
            return _container.Alerts.Find(id);
        }

        public override void Insert(Alert model)
        {
            _container.Alerts.Add(model);
        }

        public override void Delete(Alert model)
        {
            _container.Alerts.Remove(model);
        }

        public override void Update(Alert model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public override void Save()
        {
            _container.SaveChanges();
        }
    }
}
