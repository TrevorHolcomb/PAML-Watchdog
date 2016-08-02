using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories.Database;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListEscalationChainRepository : Repository<EscalationChain>
    {
        private readonly List<EscalationChain> _escalationChains;

        public ListEscalationChainRepository()
        {
            _escalationChains = new List<EscalationChain>();
        }
        public  override void Dispose()
        {
            //do nothing
        }

        public  override IEnumerable<EscalationChain> Get()
        {
            return _escalationChains.ToList();
        }

        public  override EscalationChain GetById(int id)
        {
            return _escalationChains.Find(escalationChain => escalationChain.Id == id);
        }

        public override EscalationChain GetByName(string name)
        {
            return _escalationChains.Where(chain => chain.Name == name).DefaultIfEmpty(null).First();
        }

        public  override void Insert(EscalationChain model)
        {
            _escalationChains.Add(model);
        }

        public  override void Delete(EscalationChain model)
        {
            _escalationChains.Remove(model);
        }

        public  override void Update(EscalationChain model)
        {
            //do nothing
        }

        public  override void Save()
        {
            //do nothing
        }
    }
}
