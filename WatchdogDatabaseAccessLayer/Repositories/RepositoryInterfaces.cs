using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogDatabaseAccessLayer.Repositories
{
    public interface IAlertRepository : IDisposable
    {
        IEnumerable<Alert> Get();
        Alert GetById(int id);
        void Insert(Alert model);
        void Delete(Alert model);
        void Update(Alert model);
        void Save();
    }

    public interface IMessageRepository : IDisposable
    {
        IEnumerable<Message> Get();
        Message GetById(int id);
        void Insert(Message model);
        void Delete(Message model);
        void Update(Message model);
        void Save();
    }

    public interface IMessageTypeRepository : IDisposable
    {
        IEnumerable<MessageType> Get();
        MessageType GetById(int id);
        void Insert(MessageType model);
        void Delete(MessageType model);
        void Update(MessageType model);
        void Save();
    }

    public interface IRuleRepository : IDisposable
    {
        IEnumerable<Rule> Get();
        Rule GetById(int id);
        void Insert(Rule model);
        void Delete(Rule model);
        void Update(Rule model);
        void Save();
    }
}
