using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListAlertRepository : IRepository<Alert>
    {
        private readonly List<Alert> _alerts;

        public ListAlertRepository()
        {
            _alerts = new List<Alert>();
        }
        public void Dispose()
        {
            //do nothing
        }

        public IEnumerable<Alert> Get()
        {
            return _alerts.ToList();
        }

        public Alert GetById(int id)
        {
            return _alerts.Find(Alert => Alert.Id == id);
        }

        public void Insert(Alert model)
        {
            _alerts.Add(model);
        }

        public void Delete(Alert model)
        {
            _alerts.Remove(model);
        }

        public void Update(Alert model)
        {
            //do nothing
        }

        public void Save()
        {
            //do nothing
        }
    }
}
