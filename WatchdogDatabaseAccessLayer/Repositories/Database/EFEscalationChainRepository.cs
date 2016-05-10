using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFEscalationChainRepository : Repository<EscalationChain>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFEscalationChainRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override IEnumerable<EscalationChain> Get()
        {
            return _container.EscalationChains.ToList();
        }

        public override EscalationChain GetById(int id)
        {
            return _container.EscalationChains.Find(id);
        }

        public override void Insert(EscalationChain model)
        {
            _container.EscalationChains.Add(model);
        }

        public override void Delete(EscalationChain model)
        {
            _container.EscalationChains.Remove(model);
        }

        public override void Update(EscalationChain model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public override void Save()
        {
            _container.SaveChanges();
        }

        public override void Dispose()
        {
            _container.Dispose();
        }
    }
}
