using System.Collections.Generic;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListRuleCategoryRepository : Repository<RuleCategory>
    {
        private readonly List<RuleCategory> _ruleCategories;

        public ListRuleCategoryRepository()
        {
            _ruleCategories = new List<RuleCategory>();
        }
        public override void Dispose()
        {
            //do nothing
        }

        public override IEnumerable<RuleCategory> Get()
        {
            return _ruleCategories.ToList();
        }

        public override RuleCategory GetById(int id)
        {
            return _ruleCategories.Find(ruleCategory => ruleCategory.Id == id);
        }

        public override void Insert(RuleCategory model)
        {
            _ruleCategories.Add(model);
        }

        public override void Delete(RuleCategory model)
        {
            _ruleCategories.Remove(model);
        }

        public override void Update(RuleCategory model)
        {
            //do nothing
        }

        public override void Save()
        {
            //do nothing
        }
    }
}
