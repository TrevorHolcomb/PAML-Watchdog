using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories.Database;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListAlertRepository : Repository<Alert>
    {
        private readonly List<Alert> _alerts;

        public ListAlertRepository()
        {
            _alerts = new List<Alert>();
        }
        public override void Dispose()
        {
            //do nothing
        }

        public override IEnumerable<Alert> Get()
        {
            return _alerts.ToList();
        }

        public override Alert GetById(int id)
        {
            return _alerts.Find(Alert => Alert.Id == id);
        }

        public override void Insert(Alert model)
        {
            _alerts.Add(model);
        }

        public override void Delete(Alert model)
        {
            _alerts.Remove(model);
        }

        public override void Update(Alert model)
        {
            //do nothing
        }

        public override void Save()
        {
            //do nothing
        }
    }
}
