using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFRuleRepository : Repository<Rule>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFRuleRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }
        public override void Dispose()
        {
            
        }

        public override IEnumerable<Rule> Get()
        {
            return _container.Rules.ToList();
        }

        public override Rule GetById(int id)
        {
            return _container.Rules.Find(id);
        }

        public override Rule GetByName(string name)
        {
            return _container.Rules.First(rule => rule.Name == name);
        }

        public override void Insert(Rule model)
        {
            _container.Rules.Add(model);
        }

        public override void Delete(Rule model)
        {
            _container.Entry(model).Collection<RuleCategory>("RuleCategories").Load();
            _container.Rules.Remove(model);
        }

        public override void Update(Rule model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public override void Save()
        {
            _container.SaveChanges();
        }
    }
}
