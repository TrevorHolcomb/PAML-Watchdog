using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class 
    {
        IEnumerable<TEntity> Get();
        TEntity GetById(int id);
        void Insert(TEntity model);
        void Delete(TEntity model);
        void Update(TEntity model);
        void Save();
    }
}