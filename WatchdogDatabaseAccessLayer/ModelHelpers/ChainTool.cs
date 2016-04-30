using System;
using System.Collections.Generic;
using System.Linq;

namespace WatchdogDatabaseAccessLayer
{
    public static partial class ChainTool
    {

        public static EscalationChainLink[] GetLinks(EscalationChain chain)
        {
            var links = new List<EscalationChainLink>();
            for (var node = chain.EscalationChainRootLink; node != null; node = node.NextLink)
                links.Add(node);
            return links.ToArray();
        }

        public static EscalationChain ConstructChainFromLinks(params EscalationChainLink[] links)
        {

            var linkList = links.ToList();

            for (int i = 0; i < linkList.Count; i++)
            {
                //If Not Last
                if (i < linkList.Count - 1)
                    linkList[i].NextLink = linkList[i+1];

                //If Not First
                if( i > 0)
                    linkList[i].PreviousLink = linkList[i-1];
            }

            return new EscalationChain
            {
                EscalationChainRootLink = linkList.First()
            };

        }

        public static EscalationChainLink RemoveLinkAt(this EscalationChain chain, int index)
        {
            var chainArray = GetLinks(chain);
            //If Node To Remove Has No Next Or Previous
            if (index == 0 && chainArray.Length == 1)
            {
                var ret = chain.EscalationChainRootLink;
                chain.EscalationChainRootLink = null;
                return ret;
            }
            //If Node To Remove Has a Next but no previous
            else if (index == 0 && chainArray.Length > 1)
            {
                var oldRoot = chain.EscalationChainRootLink;
                var newRoot = chain.EscalationChainRootLink.NextLink;


                chain.EscalationChainRootLink = newRoot;

                oldRoot.NextLink = null;
                oldRoot.PreviousLink = null;
                newRoot.PreviousLink = null;

                return oldRoot;
            }
            //If Node To Remove Has A Next And A Previous
            else if (index > 0 && index < chainArray.Length - 1)
            {
                var previous = chain.EscalationChainRootLink;
                for (int i = 0; i < index - 1; i++)
                    previous = previous.NextLink;

                var toRemove = previous.NextLink;
                var next = chain.EscalationChainRootLink;
                next = toRemove.NextLink;

                previous.NextLink = next;
                next.PreviousLink = previous;

                return toRemove;
            }
            //If Node To Remove Has No Next, and A Previous
            else
            {
                var link = chain.EscalationChainRootLink;
                for (int i = 0; i < chainArray.Length - 2; i++)
                    link = link.NextLink;

                var last = link.NextLink;
                last.PreviousLink = null;
                link.NextLink = null;

                return last;
            }
        }
        public static void AppendLinkAt(this EscalationChain chain, EscalationChainLink appendingChainLink, int index)
        {

            //If Node To Add Has No Next Or Previous
            if (index == 0)
            {
                var oldRoot = chain.EscalationChainRootLink;
                chain.EscalationChainRootLink = appendingChainLink;
                appendingChainLink.NextLink = oldRoot;
                if (oldRoot != null)
                    oldRoot.PreviousLink = appendingChainLink;
            }
            else
            {
                var previous = chain.EscalationChainRootLink;
                for (int i = 0; i < index - 1; i++)
                    previous = previous.NextLink;
                var next = previous.NextLink;

                previous.NextLink = appendingChainLink;
                appendingChainLink.PreviousLink = previous;

                if (next == null) return;
                appendingChainLink.NextLink = next;
                next.PreviousLink = appendingChainLink;
            }
        }
    }
}