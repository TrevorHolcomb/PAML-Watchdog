using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WatchdogDatabaseAccessLayer.Models
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

        public static EscalationChainLink[] GetLinks(EscalationChain chain)
        {
            var links = new List<EscalationChainLink>();
            for (var node = chain.EscalationChainRootLink; node != null; node = node.NextLink)
                links.Add(node);
            return links.ToArray();
        }
        
        public static EscalationChain NewChainFromLinks(params EscalationChainLink[] links)
        {
            var linkList = links.ToList();

            for (var i = 0; i < linkList.Count; i++)
            {
                //If Not Last
                if (i < linkList.Count - 1)
                    linkList[i].NextLink = linkList[i + 1];

                //If Not First
                if (i > 0)
                    linkList[i].PreviousLink = linkList[i - 1];
            }

            return new EscalationChain
            {
                EscalationChainRootLink = linkList.FirstOrDefault()
            };
        }

        public EscalationChainLink RemoveAt(int index)
        {
            var chainArray = GetLinks(this);
            //If Node To Remove Has No Next Or Previous
            if (index == 0 && chainArray.Length == 1)
            {
                var ret = EscalationChainRootLink;
                EscalationChainRootLink = null;
                return ret;
            }
            //If Node To Remove Has a Next but no previous
            if (index == 0 && chainArray.Length > 1)
            {
                var oldRoot = EscalationChainRootLink;
                var newRoot = EscalationChainRootLink.NextLink;


                EscalationChainRootLink = newRoot;

                oldRoot.NextLink = null;
                oldRoot.PreviousLink = null;
                newRoot.PreviousLink = null;

                return oldRoot;
            }
            //If Node To Remove Has A Next And A Previous
            if (index > 0 && index < chainArray.Length - 1)
            {
                var previous = EscalationChainRootLink;
                for (var i = 0; i < index - 1; i++)
                    previous = previous.NextLink;

                var toRemove = previous.NextLink;
                var next = EscalationChainRootLink;
                next = toRemove.NextLink;

                previous.NextLink = next;
                next.PreviousLink = previous;

                return toRemove;
            }
            //If Node To Remove Has No Next, and A Previous
            var link = EscalationChainRootLink;
            for (var i = 0; i < chainArray.Length - 2; i++)
                link = link.NextLink;

            var last = link.NextLink;
            last.PreviousLink = null;
            link.NextLink = null;

            return last;
        }

        public EscalationChainLink GetLinkAt(int index)
        {
            var node = EscalationChainRootLink;
            for (var i = 0; i < index; i++)
                node = node.NextLink;
            return node;
        }

        public int IndexOf(EscalationChainLink link)
        {
            var node = EscalationChainRootLink;
            for (var i = 0; i < Length(); i++)
            {
                if (node.Equals(link))
                    return i;
                node = node.NextLink;
            }
            throw new KeyNotFoundException();
        }

        public void InsertAt(EscalationChainLink appendingChainLink, int index)
        {
            //If Node To Add Has No Next Or Previous
            if (index == 0)
            {
                var oldRoot = EscalationChainRootLink;
                EscalationChainRootLink = appendingChainLink;
                appendingChainLink.NextLink = oldRoot;
                if (oldRoot != null)
                    oldRoot.PreviousLink = appendingChainLink;
            }
            else
            {
                var previous = EscalationChainRootLink;
                for (var i = 0; i < index - 1; i++)
                    previous = previous.NextLink;
                var next = previous.NextLink;

                previous.NextLink = appendingChainLink;
                appendingChainLink.PreviousLink = previous;

                if (next == null) return;
                appendingChainLink.NextLink = next;
                next.PreviousLink = appendingChainLink;
            }
        }

        public int Length()
        {
            var node = EscalationChainRootLink;
            var i = 0;
            while (node != null)
            {
                i++;
                node = node.NextLink;
            }

            return i;
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
