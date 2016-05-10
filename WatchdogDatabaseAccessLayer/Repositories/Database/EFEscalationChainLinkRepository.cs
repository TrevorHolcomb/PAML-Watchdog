using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFEscalationChainLinkRepository : IRepository<EscalationChainLink>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFEscalationChainLinkRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public IEnumerable<EscalationChainLink> Get()
        {
            return _container.EscalationChainLinks.ToList();
        }

        public EscalationChainLink GetById(int id)
        {
            return _container.EscalationChainLinks.Find(id);
        }

        public void Insert(EscalationChainLink model)
        {
            _container.EscalationChainLinks.Add(model);
        }

        public void Delete(EscalationChainLink model)
        {
            _container.EscalationChainLinks.Remove(model);
        }

        public void Update(EscalationChainLink model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public void Save()
        {
            _container.SaveChanges();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
