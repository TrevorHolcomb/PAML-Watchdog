using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFTemplatedRuleRepository : Repository<TemplatedRule>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFTemplatedRuleRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }
        public override void Dispose()
        {
            
        }

        public override IEnumerable<TemplatedRule> Get()
        {
            return _container.TemplatedRules.ToList();
        }

        public override TemplatedRule GetById(int id)
        {
            return _container.TemplatedRules.Find(id);
        }

        public override TemplatedRule GetByName(string name)
        {
            return _container.TemplatedRules.First(rule => rule.Name == name);
        }

        public override void Insert(TemplatedRule model)
        {
            _container.TemplatedRules.Add(model);
        }

        public override void Delete(TemplatedRule model)
        {
            _container.TemplatedRules.Remove(model);
        }

        public override void Update(TemplatedRule model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public override void Save()
        {
            _container.SaveChanges();
        }
    }
}