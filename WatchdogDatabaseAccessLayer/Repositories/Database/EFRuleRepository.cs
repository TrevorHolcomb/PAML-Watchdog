using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFRuleRepository : IRepository<Rule>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFRuleRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }
        public void Dispose()
        {
            _container.Dispose();
        }

        public IEnumerable<Rule> Get()
        {
            return _container.Rules.ToList();
        }

        public Rule GetById(int id)
        {
            return _container.Rules.Find(id);
        }

        public void Insert(Rule model)
        {
            _container.Rules.Add(model);
        }

        public void Delete(Rule model)
        {
            _container.Rules.Remove(model);
        }

        public void Update(Rule model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public void Save()
        {
            _container.SaveChanges();
        }
    }
}
