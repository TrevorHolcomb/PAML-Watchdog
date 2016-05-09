using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListEscalationChainRepository : IRepository<EscalationChain>
    {
        private readonly List<EscalationChain> _escalationChains;

        public ListEscalationChainRepository()
        {
            _escalationChains = new List<EscalationChain>();
        }
        public void Dispose()
        {
            //do nothing
        }

        public IEnumerable<EscalationChain> Get()
        {
            return _escalationChains.ToList();
        }

        public EscalationChain GetById(int id)
        {
            return _escalationChains.Find(escalationChain => escalationChain.Id == id);
        }

        public void Insert(EscalationChain model)
        {
            _escalationChains.Add(model);
        }

        public void Delete(EscalationChain model)
        {
            _escalationChains.Remove(model);
        }

        public void Update(EscalationChain model)
        {
            //do nothing
        }

        public void Save()
        {
            //do nothing
        }
    }
}
