using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories
{
    public interface IMessageRepository : IDisposable
    {
        IEnumerable<Message> Get();
        Message GetById(int id);
        void Insert(Message model);
        void Delete(Message model);
        void Update(Message model);
        void Save();
    }
}