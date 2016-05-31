using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFUnvalidatedMessageRepository : Repository<UnvalidatedMessage>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFUnvalidatedMessageRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override IEnumerable<UnvalidatedMessage> Get()
        {
            return _container.UnvalidatedMessages.ToList();
        }

        public override UnvalidatedMessage GetById(int id)
        {
            return _container.UnvalidatedMessages.Find(id);
        }

        public override void Insert(UnvalidatedMessage model)
        {
            _container.UnvalidatedMessages.Add(model);
        }

        public override void Delete(UnvalidatedMessage model)
        {
            _container.UnvalidatedMessages.Remove(model);
        }

        public override void Update(UnvalidatedMessage model)
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
