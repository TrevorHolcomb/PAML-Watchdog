using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFMessageTypeParameterTypeRepository : IMessageTypeParameterTypeRepository
    {
        private readonly WatchdogDatabaseContainer _container;
        public EFMessageTypeParameterTypeRepository()
        {
            _container = new WatchdogDatabaseContainer();
        }

        public IEnumerable<MessageTypeParameterType> Get()
        {
            return _container.MessageTypeParameterTypes.ToList();
        }

        public MessageTypeParameterType GetById(int id)
        {
            return _container.MessageTypeParameterTypes.Find(id);
        }

        public void Insert(MessageTypeParameterType model)
        {
            _container.MessageTypeParameterTypes.Add(model);
        }

        public void Delete(MessageTypeParameterType model)
        {
            _container.MessageTypeParameterTypes.Remove(model);
        }

        public void Update(MessageTypeParameterType model)
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
