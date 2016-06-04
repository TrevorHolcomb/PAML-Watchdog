using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListNotifyeeRepository : Repository<Notifyee>
    {
        private readonly List<Notifyee> _notifyees;

        public ListNotifyeeRepository()
        {
            _notifyees = new List<Notifyee>();
        }
        public override void Dispose()
        {
            //do nothing
        }

        public override IEnumerable<Notifyee> Get()
        {
            return _notifyees.ToList();
        }

        public override Notifyee GetById(int id)
        {
            return _notifyees.Find(notifyee => notifyee.Id == id);
        }

        public override Notifyee GetByName(string name)
        {
            return _notifyees.Where(notifyee => notifyee.Name == name).DefaultIfEmpty(null).First();
        }

        public override void Insert(Notifyee model)
        {
            _notifyees.Add(model);
        }

        public override void Delete(Notifyee model)
        {
            _notifyees.Remove(model);
        }

        public override void Update(Notifyee model)
        {
            //do nothing
        }

        public override void Save()
        {
            //do nothing
        }
    }
}
