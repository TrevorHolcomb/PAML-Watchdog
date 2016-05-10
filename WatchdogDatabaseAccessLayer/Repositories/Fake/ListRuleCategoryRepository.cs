using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListRuleCategoryRepository : Repository<RuleCategory>
    {
        private readonly List<RuleCategory> _ruleCategorys;

        public ListRuleCategoryRepository()
        {
            _ruleCategorys = new List<RuleCategory>();
        }
        public override void Dispose()
        {
            //do nothing
        }

        public override IEnumerable<RuleCategory> Get()
        {
            return _ruleCategorys.ToList();
        }

        public override RuleCategory GetById(int id)
        {
            return _ruleCategorys.Find(ruleCategory => ruleCategory.Id == id);
        }

        public override void Insert(RuleCategory model)
        {
            _ruleCategorys.Add(model);
        }

        public override void Delete(RuleCategory model)
        {
            _ruleCategorys.Remove(model);
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
