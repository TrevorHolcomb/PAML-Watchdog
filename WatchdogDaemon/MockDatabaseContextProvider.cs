using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer;

namespace Watchdog
{
    internal class MockDatabaseContextProvider : IContextProvider
    {
        private readonly ICollection<Rule> _rules;
        private readonly ICollection<Message> _messages;
        public MockDatabaseContextProvider(ICollection<Rule> rules, ICollection<Message> messages)
        {
            _rules = rules;
            _messages = messages;
        }
        public WatchdogDatabaseContainer GetDatabaseContext()
        {
            return WatchdogDatabaseContextMocker.Mock(_rules, _messages);
        }
    }
}
