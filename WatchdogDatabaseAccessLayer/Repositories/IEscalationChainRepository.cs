using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories
{
    public interface IEscalationChainRepository : IDisposable
    {
        IEnumerable<EscalationChain> Get();
        EscalationChain GetById(int id);
        void Insert(EscalationChain model);
        void Delete(EscalationChain model);
        void Update(EscalationChain model);
        void Save();
    }
}