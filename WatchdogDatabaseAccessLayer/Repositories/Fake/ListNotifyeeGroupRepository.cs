using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListNotifyeeGroupRepository : INotifyeeGroupRepository
    {
        private readonly List<NotifyeeGroup> _notifyeeGroups;

        public ListNotifyeeGroupRepository()
        {
            _notifyeeGroups = new List<NotifyeeGroup>();
        }
        public void Dispose()
        {
            //do nothing
        }

        public IEnumerable<NotifyeeGroup> Get()
        {
            return _notifyeeGroups.ToList();
        }

        public NotifyeeGroup GetById(int id)
        {
            return _notifyeeGroups.Find(notifyeeGroup => notifyeeGroup.Id == id);
        }

        public void Insert(NotifyeeGroup model)
        {
            _notifyeeGroups.Add(model);
        }

        public void Delete(NotifyeeGroup model)
        {
            _notifyeeGroups.Remove(model);
        }

        public void Update(NotifyeeGroup model)
        {
            //do nothing
        }

        public void Save()
        {
            //do nothing
        }
    }
}
