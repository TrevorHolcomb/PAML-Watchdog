using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories
{
    public interface IRuleRepository : IDisposable
    {
        IEnumerable<Rule> Get();
        Rule GetById(int id);
        void Insert(Rule model);
        void Delete(Rule model);
        void Update(Rule model);
        void Save();
    }
}