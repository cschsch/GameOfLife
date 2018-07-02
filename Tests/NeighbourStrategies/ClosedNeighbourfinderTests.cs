using System.Linq;
using Engine.Strategies.NeighbourStrategies;
using Engine.Entities.Standard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.NeighbourStrategies
{
    [TestClass]
    public class ClosedNeighbourFinderTests
    {
        [TestMethod]
        public void FindNeighbours_NoBorders_NoneAlive() =>
            NeighbourFinderBase.FindNeighbours_NoBorders_NoneAlive(new ClosedNeighbourFinder<StandardCell, StandardCellGrid>());

        [TestMethod]
        public void FindNeighbours_NoBorders_TwoAlive() =>
            NeighbourFinderBase.FindNeighbours_NoBorders_TwoAlive(new ClosedNeighbourFinder<StandardCell, StandardCellGrid>());

        [TestMethod]
        public void FindNeighbours_OnBorder_DoesNotReach() =>
            NeighbourFinderBase.FindNeighbours_OnBorder(new ClosedNeighbourFinder<StandardCell, StandardCellGrid>(), res => !res.Any(c => c.IsAlive));
    }
}