using System;
using System.Collections.Generic;

namespace WatchdogDatabaseAccessLayer.Repositories
{
    public abstract class Repository<TEntity> : IDisposable where TEntity : class
    {
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        public abstract void Dispose();
        public abstract IEnumerable<TEntity> Get();
        public abstract TEntity GetById(int id);
        public abstract void Insert(TEntity model);
        public abstract void Delete(TEntity model);
        public abstract void Update(TEntity model);
        public abstract void Save();
    }
}
