using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WatchdogDatabaseAccessLayer.ModelHelpers
{
    public class EscalationChainTests
    {

        [Fact]
        public void CanGetArray()
        {
            var first = new EscalationChainLink();
            var second = new EscalationChainLink();
            var third = new EscalationChainLink();

            var links = new EscalationChainLink[]
            {
                first, second, third
            };

            var chain = ChainTool.ConstructChainFromLinks(links);

            Assert.Equal(ChainTool.GetLinks(chain).Length, links.Length);
            Assert.Equal(ChainTool.GetLinks(chain), links);
        }

        [Fact]
        public void CanRemoveMiddleElement()
        {
            var first = new EscalationChainLink {Id=0};
            var second = new EscalationChainLink {Id=1};
            var third = new EscalationChainLink {Id=2};

            var links = new EscalationChainLink[]
            {
                first, second, third
            };
            var expectedLinks = new EscalationChainLink[]
            {
                first, third
            };

            var chain = ChainTool.ConstructChainFromLinks(links);
            ChainTool.RemoveLinkAt(chain, 1);
            var actualLinks = ChainTool.GetLinks(chain);
            Assert.Equal(expectedLinks, actualLinks);
        }

        [Fact]
        public void CanRemoveSingleElement()
        {
            
            EscalationChainLink first = new EscalationChainLink();
            var chain = ChainTool.ConstructChainFromLinks(first);

            ChainTool.RemoveLinkAt(chain,0);
            Assert.Empty(ChainTool.GetLinks(chain));
        }

        [Fact]
        public void CanRemoveLastElement()
        {
            var first = new EscalationChainLink { Id = 0 };
            var second = new EscalationChainLink { Id = 1 };
            var third = new EscalationChainLink { Id = 2 };

            var links = new EscalationChainLink[]
            {
                first, second, third
            };
            var expectedLinks = new EscalationChainLink[]
            {
                first, second
            };

            var chain = ChainTool.ConstructChainFromLinks(links);
            ChainTool.RemoveLinkAt(chain, 2);
            var actualLinks = ChainTool.GetLinks(chain);
            Assert.Equal(expectedLinks, actualLinks);
        }

        [Fact]
        public void CanInsertLastElement()
        {
            var first = new EscalationChainLink { Id = 0 };
            var second = new EscalationChainLink { Id = 1 };
            var third = new EscalationChainLink { Id = 2 };

            var links = new EscalationChainLink[]
            {
                first, second
            };
            var expectedLinks = new EscalationChainLink[]
            {
                first, second, third
            };

            var chain = ChainTool.ConstructChainFromLinks(links);
            ChainTool.AppendLinkAt(chain, third, 2);
            var actualLinks = ChainTool.GetLinks(chain);
            Assert.Equal(expectedLinks, actualLinks);
        }

        [Fact]
        public void CanInsertMiddleElement()
        {
            var first = new EscalationChainLink { Id = 0 };
            var second = new EscalationChainLink { Id = 1 };
            var third = new EscalationChainLink { Id = 2 };

            var links = new EscalationChainLink[]
            {
                first, third
            };
            var expectedLinks = new EscalationChainLink[]
            {
                first, second, third
            };

            var chain = ChainTool.ConstructChainFromLinks(links);
            ChainTool.AppendLinkAt(chain, second, 1);
            var actualLinks = ChainTool.GetLinks(chain);
            Assert.Equal(expectedLinks, actualLinks);
        }

        [Fact]
        public void CanInsertFirstElement()
        {
            var first = new EscalationChainLink { Id = 0 };
            var second = new EscalationChainLink { Id = 1 };
            var third = new EscalationChainLink { Id = 2 };

            var links = new EscalationChainLink[]
            {
                second,third
            };
            var expectedLinks = new EscalationChainLink[]
            {
                first, second, third
            };

            var chain = ChainTool.ConstructChainFromLinks(links);
            ChainTool.AppendLinkAt(chain, first, 0);
            var actualLinks = ChainTool.GetLinks(chain);
            Assert.Equal(expectedLinks, actualLinks);
        }
    }
}
