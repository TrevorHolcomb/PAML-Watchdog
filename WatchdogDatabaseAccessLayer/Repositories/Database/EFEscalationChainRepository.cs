using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFEscalationChainRepository : IRepository<EscalationChain>
    {
        private readonly WatchdogDatabaseContainer _container;
        public EFEscalationChainRepository()
        {
            _container = new WatchdogDatabaseContainer();
        }

        public IEnumerable<EscalationChain> Get()
        {
            return _container.EscalationChains.ToList();
        }

        public EscalationChain GetById(int id)
        {
            return _container.EscalationChains.Find(id);
        }

        public void Insert(EscalationChain model)
        {
            _container.EscalationChains.Add(model);
        }

        public void Delete(EscalationChain model)
        {
            _container.EscalationChains.Remove(model);
        }

        public void Update(EscalationChain model)
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
