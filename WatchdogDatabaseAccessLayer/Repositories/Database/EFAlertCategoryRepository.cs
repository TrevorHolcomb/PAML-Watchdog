using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFAlertCategoryRepository : Repository<AlertCategory>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFAlertCategoryRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override void Dispose()
        {

        }

        public override IEnumerable<AlertCategory> Get()
        {
            return _container.AlertCategories.ToList();
        }

        public override AlertCategory GetById(int id)
        {
            return _container.AlertCategories.Find(id);
        }

        public override void Insert(AlertCategory model)
        {
            _container.AlertCategories.Add(model);
        }

        public override void Delete(AlertCategory model)
        {
            _container.AlertCategories.Remove(model);
        }

        public override void Update(AlertCategory model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public override void Save()
        {
            _container.SaveChanges();
        }
    }
}