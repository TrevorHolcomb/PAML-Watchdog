using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFAlertTypeRepository : Repository<AlertType>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFAlertTypeRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override void Dispose()
        {
            _container.Dispose();
        }

        public override IEnumerable<AlertType> Get()
        {
            return _container.AlertTypes.ToList();
        }

        public override AlertType GetById(int id)
        {
            return _container.AlertTypes.Find(id);
        }

        public override void Insert(AlertType model)
        {
            _container.AlertTypes.Add(model);
        }

        public override void Delete(AlertType model)
        {
            _container.AlertTypes.Remove(model);
        }

        public override void Update(AlertType model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public override void Save()
        {
            _container.SaveChanges();
        }
    }
}
