using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFEscalationChainLinkRepository : IEscalationChainLinkRepository
    {
        private readonly WatchdogDatabaseContainer _container;
        public EFEscalationChainLinkRepository()
        {
            _container = new WatchdogDatabaseContainer();
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
