using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFAlertCategoryItemRepository : Repository<AlertCategoryItem>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFAlertCategoryItemRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override void Dispose()
        {

        }

        public override IEnumerable<AlertCategoryItem> Get()
        {
            return _container.AlertCategoryItems.ToList();
        }

        public override AlertCategoryItem GetById(int id)
        {
            return _container.AlertCategoryItems.Find(id);
        }

        public override void Insert(AlertCategoryItem model)
        {
            _container.AlertCategoryItems.Add(model);
        }

        public override void Delete(AlertCategoryItem model)
        {
            _container.AlertCategoryItems.Remove(model);
        }

        public override void Update(AlertCategoryItem model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public override void Save()
        {
            _container.SaveChanges();
        }
    }
}