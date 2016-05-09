using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListRuleCategoryRepository : IRuleCategoryRepository
    {
        private readonly List<RuleCategory> _ruleCategorys;

        public ListRuleCategoryRepository()
        {
            _ruleCategorys = new List<RuleCategory>();
        }
        public void Dispose()
        {
            //do nothing
        }

        public IEnumerable<RuleCategory> Get()
        {
            return _ruleCategorys.ToList();
        }

        public RuleCategory GetById(int id)
        {
            return _ruleCategorys.Find(ruleCategory => ruleCategory.Id == id);
        }

        public void Insert(RuleCategory model)
        {
            _ruleCategorys.Add(model);
        }

        public void Delete(RuleCategory model)
        {
            _ruleCategorys.Remove(model);
        }

        public void Update(RuleCategory model)
        {
            //do nothing
        }

        public void Save()
        {
            //do nothing
        }
    }
}
