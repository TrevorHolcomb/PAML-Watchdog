using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFAlertRepository : IAlertRepository
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFAlertRepository()
        {
            _container = new WatchdogDatabaseContainer();
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public IEnumerable<Alert> Get()
        {
            return _container.Alerts.ToList();
        }

        public Alert GetById(int id)
        {
            return _container.Alerts.Find(id);
        }

        public void Insert(Alert model)
        {
            _container.Alerts.Add(model);
        }

        public void Delete(Alert model)
        {
            _container.Alerts.Remove(model);
        }

        public void Update(Alert model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public void Save()
        {
            _container.SaveChanges();
        }
    }
}
