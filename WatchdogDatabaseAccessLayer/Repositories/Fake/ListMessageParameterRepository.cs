using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories.Database;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListMessageParameterRepository : Repository<MessageParameter>
    {
        private readonly List<MessageParameter> _messageParameters;

        public ListMessageParameterRepository()
        {
            _messageParameters = new List<MessageParameter>();
        }
        public  override void Dispose()
        {
            //do nothing
        }

        public  override IEnumerable<MessageParameter> Get()
        {
            return _messageParameters.ToList();
        }

        public  override MessageParameter GetById(int id)
        {
            return _messageParameters.Find(messageParameter => messageParameter.Id == id);
        }

        public  override void Insert(MessageParameter model)
        {
            _messageParameters.Add(model);
        }

        public  override void Delete(MessageParameter model)
        {
            _messageParameters.Remove(model);
        }

        public  override void Update(MessageParameter model)
        {
            //do nothing
        }

        public  override void Save()
        {
            //do nothing
        }
    }
}
