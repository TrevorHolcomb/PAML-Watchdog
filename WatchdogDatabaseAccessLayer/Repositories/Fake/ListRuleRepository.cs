using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListRuleRepository : Repository<Rule>
    {
        private readonly List<Rule> _rules;

        public ListRuleRepository()
        {
            _rules = new List<Rule>();
        }
        public override void Dispose()
        {
            //do nothing
        }

        public override IEnumerable<Rule> Get()
        {
            return _rules.ToList();
        }

        public override Rule GetById(int id)
        {
            return _rules.Find(Rule => Rule.Id == id);
        }

        public override Rule GetByName(string name)
        {
            return _rules.Where(rule => rule.Name == name).DefaultIfEmpty(null).First();
        }

        public override void Insert(Rule model)
        {
            _rules.Add(model);
        }

        public override void Delete(Rule model)
        {
            _rules.Remove(model);
        }

        public override void Update(Rule model)
        {
            //do nothing
        }

        public override void Save()
        {
            //do nothing
        }
    }
}
