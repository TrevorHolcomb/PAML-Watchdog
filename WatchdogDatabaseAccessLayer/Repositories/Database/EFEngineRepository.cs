using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFEngineRepository : Repository<Engine>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFEngineRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override void Dispose()
        {
            
        }

        public override IEnumerable<Engine> Get()
        {
            return _container.Engines.ToList();
        }

        public override Engine GetByName(string name)
        {
            return _container.Engines.FirstOrDefault(engine => engine.Name == name);
        }

        public override void Insert(Engine model)
        {
            _container.Engines.Add(model);
        }

        public override void Delete(Engine model)
        {
            _container.Engines.Remove(model);
        }

        public override void Update(Engine model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public override void Save()
        {
            _container.SaveChanges();
        }
    }
}
