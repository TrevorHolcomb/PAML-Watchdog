using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFAlertTypeRepository : IAlertTypeRepository
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFAlertTypeRepository()
        {
            _container = new WatchdogDatabaseContainer();
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public IEnumerable<AlertType> Get()
        {
            return _container.AlertTypes.ToList();
        }

        public AlertType GetById(int id)
        {
            return _container.AlertTypes.Find(id);
        }

        public void Insert(AlertType model)
        {
            _container.AlertTypes.Add(model);
        }

        public void Delete(AlertType model)
        {
            _container.AlertTypes.Remove(model);
        }

        public void Update(AlertType model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public void Save()
        {
            _container.SaveChanges();
        }
    }
}
