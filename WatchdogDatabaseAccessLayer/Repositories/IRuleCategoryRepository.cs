using System;
using System.Collections.Generic;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories
{
    public interface IRuleCategoryRepository : IDisposable
    {
        IEnumerable<RuleCategory> Get();
        RuleCategory GetById(int id);
        void Insert(RuleCategory model);
        void Delete(RuleCategory model);
        void Update(RuleCategory model);
        void Save();
    }
}