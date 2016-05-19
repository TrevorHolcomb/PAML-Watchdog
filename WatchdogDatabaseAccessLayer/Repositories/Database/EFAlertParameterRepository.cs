using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFAlertParameterRepository : Repository<AlertParameter>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFAlertParameterRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override IEnumerable<AlertParameter> Get()
        {
            return _container.AlertParameters.ToList();
        }

        public override AlertParameter GetById(int id)
        {
            return _container.AlertParameters.Find(id);
        }

        public override void Insert(AlertParameter model)
        {
            _container.AlertParameters.Add(model);
        }

        public override void Delete(AlertParameter model)
        {
            _container.AlertParameters.Remove(model);
        }

        public override void Update(AlertParameter model)
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
