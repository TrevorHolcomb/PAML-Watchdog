using System.Collections.Generic;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListAlertParameterRepository : Repository<AlertParameter>
    {
        private readonly List<AlertParameter> _AlertParameters;

        public ListAlertParameterRepository()
        {
            _AlertParameters = new List<AlertParameter>();
        }
        public  override void Dispose()
        {
            //do nothing
        }

        public  override IEnumerable<AlertParameter> Get()
        {
            return _AlertParameters.ToList();
        }

        public  override AlertParameter GetById(int id)
        {
            return _AlertParameters.Find(AlertParameter => AlertParameter.Id == id);
        }

        public  override void Insert(AlertParameter model)
        {
            _AlertParameters.Add(model);
        }

        public  override void Delete(AlertParameter model)
        {
            _AlertParameters.Remove(model);
        }

        public  override void Update(AlertParameter model)
        {
            //do nothing
        }

        public  override void Save()
        {
            //do nothing
        }
    }
}
