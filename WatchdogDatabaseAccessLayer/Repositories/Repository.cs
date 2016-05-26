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

        /// <summary>
        /// WARNING: Make sure the TEntity held by this Repository does have an "Id" property before you call this 
        /// </summary>
        public virtual TEntity GetById(int id) { throw new NotSupportedException("This class does not have an \"Id\" property."); }

        /// <summary>
        /// WARNING: Make sure the TEntity held by this Repository does have a "Name" property before you call this 
        /// </summary>
        public virtual TEntity GetByName(string name) { throw new NotSupportedException("This class does not have a \"Name\" property."); }

        public abstract void Insert(TEntity model);
        public abstract void Delete(TEntity model);
        public abstract void Update(TEntity model);
        public abstract void Save();
    }
}
