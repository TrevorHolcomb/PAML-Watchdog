using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListEscalationChainLinkRepository : IRepository<EscalationChainLink>
    {
        private readonly List<EscalationChainLink> _escalationChainLinks;

        public ListEscalationChainLinkRepository()
        {
            _escalationChainLinks = new List<EscalationChainLink>();
        }
        public void Dispose()
        {
            //do nothing
        }

        public IEnumerable<EscalationChainLink> Get()
        {
            return _escalationChainLinks.ToList();
        }

        public EscalationChainLink GetById(int id)
        {
            return _escalationChainLinks.Find(EscalationChainLink => EscalationChainLink.Id == id);
        }

        public void Insert(EscalationChainLink model)
        {
            _escalationChainLinks.Add(model);
        }

        public void Delete(EscalationChainLink model)
        {
            _escalationChainLinks.Remove(model);
        }

        public void Update(EscalationChainLink model)
        {
            //do nothing
        }

        public void Save()
        {
            //do nothing
        }
    }
}
