using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFRuleTemplateRepository : Repository<RuleTemplate>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFRuleTemplateRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }
        public override void Dispose()
        {
            
        }

        public override IEnumerable<RuleTemplate> Get()
        {
            return _container.RuleTemplates.ToList();
        }

        public override RuleTemplate GetById(int id)
        {
            return _container.RuleTemplates.Find(id);
        }

        public override RuleTemplate GetByName(string name)
        {
            return _container.RuleTemplates.First(ruleTemplate => ruleTemplate.Name == name);
        }

        public override void Insert(RuleTemplate model)
        {
            _container.RuleTemplates.Add(model);
        }

        public override void Delete(RuleTemplate model)
        {
            _container.RuleTemplates.Remove(model);
        }

        public override void Update(RuleTemplate model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public override void Save()
        {
            _container.SaveChanges();
        }
    }
}
