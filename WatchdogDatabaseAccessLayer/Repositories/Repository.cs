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
        /// DO NOT CALL: This class does not have an "Id" property.
        /// </summary>
        public virtual TEntity GetById(int id) { throw new NotSupportedException("This class does not have an \"Id\" property."); }

        /// <summary>
        /// DO NOT CALL: This class does not have a "Name" property.
        /// </summary>
        public virtual TEntity GetByName(string name) { throw new NotSupportedException("This class does not have a \"Name\" property."); }

        public abstract void Insert(TEntity model);
        public abstract void Delete(TEntity model);
        public abstract void Update(TEntity model);
        public abstract void Save();
    }
}
