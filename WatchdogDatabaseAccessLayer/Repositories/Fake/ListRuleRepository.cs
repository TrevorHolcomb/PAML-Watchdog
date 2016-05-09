using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListRuleRepository : IRepository<Rule>
    {
        private readonly List<Rule> _rules;

        public ListRuleRepository()
        {
            _rules = new List<Rule>();
        }
        public void Dispose()
        {
            //do nothing
        }

        public IEnumerable<Rule> Get()
        {
            return _rules.ToList();
        }

        public Rule GetById(int id)
        {
            return _rules.Find(Rule => Rule.Id == id);
        }

        public void Insert(Rule model)
        {
            _rules.Add(model);
        }

        public void Delete(Rule model)
        {
            _rules.Remove(model);
        }

        public void Update(Rule model)
        {
            //do nothing
        }

        public void Save()
        {
            //do nothing
        }
    }
}
