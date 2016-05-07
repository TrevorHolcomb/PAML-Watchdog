using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories
{
    public interface INotifyeeGroupRepository : IDisposable
    {
        IEnumerable<NotifyeeGroup> Get();
        NotifyeeGroup GetById(int id);
        void Insert(NotifyeeGroup model);
        void Delete(NotifyeeGroup model);
        void Update(NotifyeeGroup model);
        void Save();
    }
}