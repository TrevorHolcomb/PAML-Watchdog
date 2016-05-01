using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WatchdogDatabaseAccessLayer.ModelHelpers
{
    public class ChainToolTests
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
            chain.RemoveAt(1);
            var actualLinks = ChainTool.GetLinks(chain);
            Assert.Equal(expectedLinks, actualLinks);
        }

        [Fact]
        public void CanRemoveSingleElement()
        {
            
            var first = new EscalationChainLink();
            var chain = ChainTool.ConstructChainFromLinks(first);

            chain.RemoveAt(0);
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
            chain.RemoveAt(2);
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
            chain.InsertAt(third, 2);
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
            ChainTool.InsertAt(chain, second, 1);
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
            ChainTool.InsertAt(chain, first, 0);
            var actualLinks = ChainTool.GetLinks(chain);
            Assert.Equal(expectedLinks, actualLinks);
        }


        public static TheoryData<EscalationChain, int> GetLengthTestData = new TheoryData<EscalationChain, int>
        {
            {
                ChainTool.ConstructChainFromLinks(new EscalationChainLink(), new EscalationChainLink()), 2
            },
            {
                ChainTool.ConstructChainFromLinks(),0
            },
            {
                ChainTool.ConstructChainFromLinks(new EscalationChainLink(), new EscalationChainLink(), new EscalationChainLink(), new EscalationChainLink()), 4
            }
        };

        [Theory]
        [MemberData(nameof(GetLengthTestData))]
        public void GetLengthTest(EscalationChain chain, int length)
        {
            Assert.Equal(chain.Length(), length);
        }

        public static TheoryData<EscalationChain, int, int> GetElementAtData = new TheoryData<EscalationChain, int, int>
        {
            {
                ChainTool.ConstructChainFromLinks(new EscalationChainLink {Id = 0}, new EscalationChainLink {Id = 1}, new EscalationChainLink {Id = 2}), 2, 2 
            },
            {
                ChainTool.ConstructChainFromLinks(new EscalationChainLink {Id=0}, new EscalationChainLink {Id=1}),1,1
            },
            {
                ChainTool.ConstructChainFromLinks(new EscalationChainLink {Id = 0}),0,0
            }
        };

        [Theory]
        [MemberData(nameof(GetElementAtData))]
        public void GetElementAtTest(EscalationChain chain, int index, int expectedId)
        {
            Assert.Equal(chain.GetLinkAt(index).Id, expectedId);
        }


        public static TheoryData<EscalationChain, EscalationChainLink, int> GetIndexOfLinkData = new TheoryData
            <EscalationChain, EscalationChainLink, int>
        {
            {
                ChainTool.ConstructChainFromLinks(new EscalationChainLink {Id = 0}), new EscalationChainLink {Id = 0}, 0
            },
            {
                ChainTool.ConstructChainFromLinks(new EscalationChainLink {Id = 0}, new EscalationChainLink {Id = 1}, new EscalationChainLink {Id=2}), new EscalationChainLink {Id=1}, 1
            }
        };

        [Theory]
        [MemberData(nameof(GetIndexOfLinkData))]
        public void GetIndexOfLink(EscalationChain chain, EscalationChainLink link, int expectedIndex)
        {
            Assert.Equal(chain.IndexOf(link), expectedIndex);
        }
    }
}
