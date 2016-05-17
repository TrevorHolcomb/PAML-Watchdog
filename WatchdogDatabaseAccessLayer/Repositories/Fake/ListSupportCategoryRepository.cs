using System.Collections.Generic;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListSupportCategoryRepository : Repository<SupportCategory>
    {
        private readonly List<SupportCategory> _SupportCategories;

        public ListSupportCategoryRepository()
        {
            _SupportCategories = new List<SupportCategory>();
        }
        public override void Dispose()
        {
            //do nothing
        }

        public override IEnumerable<SupportCategory> Get()
        {
            return _SupportCategories.ToList();
        }

        public override SupportCategory GetById(int id)
        {
            return _SupportCategories.Find(SupportCategory => SupportCategory.Id == id);
        }

        public override void Insert(SupportCategory model)
        {
            _SupportCategories.Add(model);
        }

        public override void Delete(SupportCategory model)
        {
            _SupportCategories.Remove(model);
        }

        public override void Update(SupportCategory model)
        {
            //do nothing
        }

        public override void Save()
        {
            //do nothing
        }
    }
}
