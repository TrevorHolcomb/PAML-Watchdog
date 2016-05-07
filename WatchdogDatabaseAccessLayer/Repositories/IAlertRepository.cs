using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories
{
    public interface IAlertRepository : IDisposable
    {
        IEnumerable<Alert> Get();
        Alert GetById(int id);
        void Insert(Alert model);
        void Delete(Alert model);
        void Update(Alert model);
        void Save();
    }
}