using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories
{
    public interface IAlertTypeRepository : IDisposable
    {
        IEnumerable<AlertType> Get();
        AlertType GetById(int id);
        void Insert(AlertType model);
        void Delete(AlertType model);
        void Update(AlertType model);
        void Save();
    }
}