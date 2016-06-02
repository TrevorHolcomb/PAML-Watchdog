using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Database
{
    public class EFMessageParameterRepository : Repository<MessageParameter>
    {
        private readonly WatchdogDatabaseContainer _container;

        public EFMessageParameterRepository(WatchdogDatabaseContainer container)
        {
            _container = container;
        }

        public override IEnumerable<MessageParameter> Get()
        {
            return _container.MessageParameters.ToList();
        }

        public override MessageParameter GetById(int id)
        {
            return _container.MessageParameters.Find(id);
        }

        public override void Insert(MessageParameter model)
        {
            _container.MessageParameters.Add(model);
        }

        public override void Delete(MessageParameter model)
        {
            _container.MessageParameters.Remove(model);
        }

        public override void Update(MessageParameter model)
        {
            _container.Entry(model).State = EntityState.Modified;
        }

        public override void Save()
        {
            _container.SaveChanges();
        }

        public override void Dispose()
        {
            
        }
    }
}
