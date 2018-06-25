using System.Linq;
using GameOfLife.Core.NeighbourStrategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.NeighbourStrategies
{
    [TestClass]
    public class ClosedNeighbourFinderTests
    {
        [TestMethod]
        public void FindNeighbours_NoBorders_NoneAlive() =>
            NeighbourFinderBase.FindNeighbours_NoBorders_NoneAlive(new ClosedNeighbourFinder());

        [TestMethod]
        public void FindNeighbours_NoBorders_TwoAlive() =>
            NeighbourFinderBase.FindNeighbours_NoBorders_TwoAlive(new ClosedNeighbourFinder());

        [TestMethod]
        public void FindNeighbours_OnBorder_DoesNotReach() =>
            NeighbourFinderBase.FindNeighbours_OnBorder(new ClosedNeighbourFinder(), res => !res.Any(c => c.IsAlive));
    }
}