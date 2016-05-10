using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFMessageTypeParameterTypeRepository : Repository<MessageTypeParameterType>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFMessageTypeParameterTypeRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override IEnumerable<MessageTypeParameterType> Get()
        {
            return _container.MessageTypeParameterTypes.ToList();
        }

        public override MessageTypeParameterType GetById(int id)
        {
            return _container.MessageTypeParameterTypes.Find(id);
        }

        public override void Insert(MessageTypeParameterType model)
        {
            _container.MessageTypeParameterTypes.Add(model);
        }

        public override void Delete(MessageTypeParameterType model)
        {
            _container.MessageTypeParameterTypes.Remove(model);
        }

        public override void Update(MessageTypeParameterType model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public override void Save()
        {
            _container.SaveChanges();
        }

        public override void Dispose()
        {
            _container.Dispose();
        }
    }
}
