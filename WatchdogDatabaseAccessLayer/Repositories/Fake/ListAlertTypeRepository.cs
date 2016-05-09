using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListAlertTypeRepository : IRepository<AlertType>
    {
        private readonly List<AlertType> _alertTypes;

        public ListAlertTypeRepository()
        {
            _alertTypes = new List<AlertType>();
        }
        public void Dispose()
        {
            //do nothing
        }

        public IEnumerable<AlertType> Get()
        {
            return _alertTypes.ToList();
        }

        public AlertType GetById(int id)
        {
            return _alertTypes.Find(alertType => alertType.Id == id);
        }

        public void Insert(AlertType model)
        {
            _alertTypes.Add(model);
        }

        public void Delete(AlertType model)
        {
            _alertTypes.Remove(model);
        }

        public void Update(AlertType model)
        {
            //do nothing
        }

        public void Save()
        {
            //do nothing
        }
    }
}
