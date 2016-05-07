using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories
{
    public interface IEscalationChainLinkRepository : IDisposable
    {
        IEnumerable<EscalationChainLink> Get();
        EscalationChainLink GetById(int id);
        void Insert(EscalationChainLink model);
        void Delete(EscalationChainLink model);
        void Update(EscalationChainLink model);
        void Save();
    }
}