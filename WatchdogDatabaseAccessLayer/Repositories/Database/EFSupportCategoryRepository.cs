using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFSupportCategoryRepository : Repository<SupportCategory>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFSupportCategoryRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override IEnumerable<SupportCategory> Get()
        {
            return _container.SupportCategories.ToList();
        }

        public override SupportCategory GetById(int id)
        {
            return _container.SupportCategories.Find(id);
        }

        public override SupportCategory GetByName(string name)
        {
            return _container.SupportCategories.Where(category => category.Name == name).DefaultIfEmpty(null).First();
        }

        public override void Insert(SupportCategory model)
        {
            _container.SupportCategories.Add(model);
        }

        public override void Delete(SupportCategory model)
        {
            _container.SupportCategories.Remove(model);
        }

        public override void Update(SupportCategory model)
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
