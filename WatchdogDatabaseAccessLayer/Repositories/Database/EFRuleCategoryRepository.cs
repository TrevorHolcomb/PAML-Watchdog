using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFRuleCategoryRepository : IRepository<RuleCategory>
    {
        private readonly WatchdogDatabaseContainer _container;
        public EFRuleCategoryRepository()
        {
            _container = new WatchdogDatabaseContainer();
        }

        public IEnumerable<RuleCategory> Get()
        {
            return _container.RuleCategories.ToList();
        }

        public RuleCategory GetById(int id)
        {
            return _container.RuleCategories.Find(id);
        }

        public void Insert(RuleCategory model)
        {
            _container.RuleCategories.Add(model);
        }

        public void Delete(RuleCategory model)
        {
            _container.RuleCategories.Remove(model);
        }

        public void Update(RuleCategory model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public void Save()
        {
            _container.SaveChanges();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
