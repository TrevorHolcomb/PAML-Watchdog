using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories.Fake
{
    public class ListMessageParameterRepository : IMessageParameterRepository
    {
        private readonly List<MessageParameter> _messageParameters;

        public ListMessageParameterRepository()
        {
            _messageParameters = new List<MessageParameter>();
        }
        public void Dispose()
        {
            //do nothing
        }

        public IEnumerable<MessageParameter> Get()
        {
            return _messageParameters.ToList();
        }

        public MessageParameter GetById(int id)
        {
            return _messageParameters.Find(messageParameter => messageParameter.Id == id);
        }

        public void Insert(MessageParameter model)
        {
            _messageParameters.Add(model);
        }

        public void Delete(MessageParameter model)
        {
            _messageParameters.Remove(model);
        }

        public void Update(MessageParameter model)
        {
            //do nothing
        }

        public void Save()
        {
            //do nothing
        }
    }
}
