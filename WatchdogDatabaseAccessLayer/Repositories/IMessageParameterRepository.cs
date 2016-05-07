using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories
{
    public interface IMessageParameterRepository : IDisposable
    {
        IEnumerable<MessageParameter> Get();
        MessageParameter GetById(int id);
        void Insert(MessageParameter model);
        void Delete(MessageParameter model);
        void Update(MessageParameter model);
        void Save();
    }
}