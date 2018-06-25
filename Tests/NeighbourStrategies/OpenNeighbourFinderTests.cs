using System.Linq;
using GameOfLife.Core.NeighbourStrategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.NeighbourStrategies
{
    [TestClass]
    public class OpenNeighbourFinderTests
    {
        [TestMethod]
        public void FindNeighbours_NoBorders_NoneAlive() =>
            NeighbourFinderBase.FindNeighbours_NoBorders_NoneAlive(new OpenNeighbourFinder());

        [TestMethod]
        public void FindNeighbours_NoBorders_TwoAlive() =>
            NeighbourFinderBase.FindNeighbours_NoBorders_TwoAlive(new OpenNeighbourFinder());

        [TestMethod]
        public void FindNeighbours_OnBorder_ReachesOver() =>
            NeighbourFinderBase.FindNeighbours_OnBorder(new OpenNeighbourFinder(), res => res.Count(c => c.IsAlive) == 2);
    }
}