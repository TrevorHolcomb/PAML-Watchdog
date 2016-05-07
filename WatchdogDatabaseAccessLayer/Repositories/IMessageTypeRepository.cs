using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories
{
    public interface IMessageTypeRepository : IDisposable
    {
        IEnumerable<MessageType> Get();
        MessageType GetById(int id);
        void Insert(MessageType model);
        void Delete(MessageType model);
        void Update(MessageType model);
        void Save();
    }
}