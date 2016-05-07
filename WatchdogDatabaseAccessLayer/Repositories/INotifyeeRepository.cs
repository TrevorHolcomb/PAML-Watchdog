using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories
{
    public interface INotifyeeRepository : IDisposable
    {
        IEnumerable<Notifyee> Get();
        Notifyee GetById(int id);
        void Insert(Notifyee model);
        void Delete(Notifyee model);
        void Update(Notifyee model);
        void Save();
    }
}