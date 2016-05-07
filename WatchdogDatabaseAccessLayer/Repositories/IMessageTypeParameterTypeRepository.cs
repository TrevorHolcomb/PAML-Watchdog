using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories
{
    public interface IMessageTypeParameterTypeRepository : IDisposable
    {
        IEnumerable<MessageTypeParameterType> Get();
        MessageTypeParameterType GetById(int id);
        void Insert(MessageTypeParameterType model);
        void Delete(MessageTypeParameterType model);
        void Update(MessageTypeParameterType model);
        void Save();
    }
}