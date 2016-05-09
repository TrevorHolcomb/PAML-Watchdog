using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListNotifyeeRepository : IRepository<Notifyee>
    {
        private readonly List<Notifyee> _notifyees;

        public ListNotifyeeRepository()
        {
            _notifyees = new List<Notifyee>();
        }
        public void Dispose()
        {
            //do nothing
        }

        public IEnumerable<Notifyee> Get()
        {
            return _notifyees.ToList();
        }

        public Notifyee GetById(int id)
        {
            return _notifyees.Find(notifyee => notifyee.Id == id);
        }

        public void Insert(Notifyee model)
        {
            _notifyees.Add(model);
        }

        public void Delete(Notifyee model)
        {
            _notifyees.Remove(model);
        }

        public void Update(Notifyee model)
        {
            //do nothing
        }

        public void Save()
        {
            //do nothing
        }
    }
}
