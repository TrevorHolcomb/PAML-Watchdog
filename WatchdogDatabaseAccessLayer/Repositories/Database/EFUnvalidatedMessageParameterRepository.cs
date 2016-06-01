using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFUnvalidatedMessageParameterRepository : Repository<UnvalidatedMessageParameter>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFUnvalidatedMessageParameterRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override IEnumerable<UnvalidatedMessageParameter> Get()
        {
            return _container.UnvalidatedMessageParameters.ToList();
        }

        public override UnvalidatedMessageParameter GetById(int id)
        {
            return _container.UnvalidatedMessageParameters.Find(id);
        }

        public override void Insert(UnvalidatedMessageParameter model)
        {
            _container.UnvalidatedMessageParameters.Add(model);
        }

        public override void Delete(UnvalidatedMessageParameter model)
        {
            _container.UnvalidatedMessageParameters.Remove(model);
        }

        public override void Update(UnvalidatedMessageParameter model)
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
