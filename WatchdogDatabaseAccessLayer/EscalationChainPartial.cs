using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer.ModelHelpers;

namespace WatchdogDatabaseAccessLayer
{
    public partial class EscalationChain : IEnumerable<EscalationChainLink>
    {
        public IEnumerator<EscalationChainLink> GetEnumerator()
        {
            return new EscalationChainEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class EscalationChainEnumerator : IEnumerator<EscalationChainLink>
    {
        private readonly EscalationChain _chain;
        private int _index;
        public EscalationChainEnumerator(EscalationChain chain)
        {
            _chain = chain;
            _index = 0;
        }

        public void Dispose()
        {
            // Do nothing!
        }

        public bool MoveNext()
        {
            if (_index < _chain.Length() - 1)
            {
                _index++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            _index = 0;
        }

        public EscalationChainLink Current => _chain.GetLinkAt(_index);

        object IEnumerator.Current => Current;
    }
}
