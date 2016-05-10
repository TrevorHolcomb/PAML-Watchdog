using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories.Database;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListEscalationChainLinkRepository : Repository<EscalationChainLink>
    {
        private readonly List<EscalationChainLink> _escalationChainLinks;

        public ListEscalationChainLinkRepository()
        {
            _escalationChainLinks = new List<EscalationChainLink>();
        }
        public  override void Dispose()
        {
            //do nothing
        }

        public  override IEnumerable<EscalationChainLink> Get()
        {
            return _escalationChainLinks.ToList();
        }

        public  override EscalationChainLink GetById(int id)
        {
            return _escalationChainLinks.Find(EscalationChainLink => EscalationChainLink.Id == id);
        }

        public  override void Insert(EscalationChainLink model)
        {
            _escalationChainLinks.Add(model);
        }

        public  override void Delete(EscalationChainLink model)
        {
            _escalationChainLinks.Remove(model);
        }

        public  override void Update(EscalationChainLink model)
        {
            //do nothing
        }

        public  override void Save()
        {
            //do nothing
        }
    }
}
