using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFEscalationChainLinkRepository : Repository<EscalationChainLink>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFEscalationChainLinkRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override IEnumerable<EscalationChainLink> Get()
        {
            return _container.EscalationChainLinks.ToList();
        }

        public override EscalationChainLink GetById(int id)
        {
            return _container.EscalationChainLinks.Find(id);
        }

        public override void Insert(EscalationChainLink model)
        {
            _container.EscalationChainLinks.Add(model);
        }

        public override void Delete(EscalationChainLink model)
        {
            _container.EscalationChainLinks.Remove(model);
        }

        public override void Update(EscalationChainLink model)
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
