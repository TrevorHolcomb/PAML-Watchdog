using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories.Database;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListEngineRepository : Repository<Engine>
    {
        private readonly List<Engine> _engines;

        public ListEngineRepository()
        {
            _engines = new List<Engine>();
        }
        public override void Dispose()
        {
            //do nothing
        }

        public override IEnumerable<Engine> Get()
        {
            return _engines.ToList();
        }

        public override Engine GetByName(string name)
        {
            return _engines.FirstOrDefault(engine =>engine.Name == name);
        }

        public override void Insert(Engine model)
        {
            _engines.Add(model);
        }

        public override void Delete(Engine model)
        {
            _engines.Remove(model);
        }

        public override void Update(Engine model)
        {
            //do nothing
        }

        public override void Save()
        {
            //do nothing
        }
    }
}
