using System;
using System.Collections.Generic;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListAlertStatusRepository : Repository<AlertStatus>
    {
        private readonly List<AlertStatus> _alertStatuses;

        public ListAlertStatusRepository()
        {
            _alertStatuses = new List<AlertStatus>();
        }
        public override void Dispose()
        {
            //do nothing
        }

        public override IEnumerable<AlertStatus> Get()
        {
            return _alertStatuses.ToList();
        }

        public override AlertStatus GetById(int id)
        {
            return _alertStatuses.Find(alertStatus => alertStatus.Id == id);
        }

        public override void Insert(AlertStatus model)
        {
            _alertStatuses.Add(model);
        }

        public override void Delete(AlertStatus model)
        {
            _alertStatuses.Remove(model);
        }

        public override void Update(AlertStatus model)
        {
            //do nothing
        }

        public override void Save()
        {
            //do nothing
        }
    }
}
