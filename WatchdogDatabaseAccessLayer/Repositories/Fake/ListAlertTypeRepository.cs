using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories.Database;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListAlertTypeRepository : Repository<AlertType>
    {
        private readonly List<AlertType> _alertTypes;

        public ListAlertTypeRepository()
        {
            _alertTypes = new List<AlertType>();
        }
        public  override void Dispose()
        {
            //do nothing
        }

        public  override IEnumerable<AlertType> Get()
        {
            return _alertTypes.ToList();
        }

        public  override AlertType GetById(int id)
        {
            return _alertTypes.Find(alertType => alertType.Id == id);
        }

        public override AlertType GetByName(string name)
        {
            return _alertTypes.FirstOrDefault(alertType => alertType.Name == name);
        }

        public  override void Insert(AlertType model)
        {
            _alertTypes.Add(model);
        }

        public  override void Delete(AlertType model)
        {
            _alertTypes.Remove(model);
        }

        public  override void Update(AlertType model)
        {
            //do nothing
        }

        public  override void Save()
        {
            //do nothing
        }
    }
}
