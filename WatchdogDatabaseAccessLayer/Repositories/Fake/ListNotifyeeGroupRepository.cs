using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListNotifyeeGroupRepository : Repository<NotifyeeGroup>
    {
        private readonly List<NotifyeeGroup> _notifyeeGroups;

        public ListNotifyeeGroupRepository()
        {
            _notifyeeGroups = new List<NotifyeeGroup>();
        }
        public override void Dispose()
        {
            //do nothing
        }

        public override IEnumerable<NotifyeeGroup> Get()
        {
            return _notifyeeGroups.ToList();
        }

        public override NotifyeeGroup GetById(int id)
        {
            return _notifyeeGroups.Find(notifyeeGroup => notifyeeGroup.Id == id);
        }

        public override void Insert(NotifyeeGroup model)
        {
            _notifyeeGroups.Add(model);
        }

        public override void Delete(NotifyeeGroup model)
        {
            _notifyeeGroups.Remove(model);
        }

        public override void Update(NotifyeeGroup model)
        {
            //do nothing
        }

        public override void Save()
        {
            //do nothing
        }
    }
}
