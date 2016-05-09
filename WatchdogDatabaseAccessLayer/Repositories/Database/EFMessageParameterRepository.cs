using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFMessageParameterRepository : IMessageParameterRepository
    {
        private readonly WatchdogDatabaseContainer _container;
        public EFMessageParameterRepository()
        {
            _container = new WatchdogDatabaseContainer();
        }

        public IEnumerable<MessageParameter> Get()
        {
            return _container.MessageParameters.ToList();
        }

        public MessageParameter GetById(int id)
        {
            return _container.MessageParameters.Find(id);
        }

        public void Insert(MessageParameter model)
        {
            _container.MessageParameters.Add(model);
        }

        public void Delete(MessageParameter model)
        {
            _container.MessageParameters.Remove(model);
        }

        public void Update(MessageParameter model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public void Save()
        {
            _container.SaveChanges();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
