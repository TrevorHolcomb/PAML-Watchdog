using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFRuleCategoryRepository : Repository<RuleCategory>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFRuleCategoryRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override IEnumerable<RuleCategory> Get()
        {
            return _container.RuleCategories.ToList();
        }

        public override RuleCategory GetById(int id)
        {
            return _container.RuleCategories.Find(id);
        }

        public override RuleCategory GetByName(string name)
        {
            return _container.RuleCategories.FirstOrDefault(category => category.Name == name);
        }

        public override void Insert(RuleCategory model)
        {
            _container.RuleCategories.Add(model);
        }

        public override void Delete(RuleCategory model)
        {
            _container.RuleCategories.Remove(model);
        }

        public override void Update(RuleCategory model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public override void Save()
        {
            _container.SaveChanges();
        }

        public override void Dispose()
        {
            
        }
    }
}
