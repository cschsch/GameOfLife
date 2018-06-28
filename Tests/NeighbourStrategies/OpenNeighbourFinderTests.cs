using System.Linq;
using Engine.Core.NeighbourStrategies;
using Engine.Entities.Standard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.NeighbourStrategies
{
    [TestClass]
    public class OpenNeighbourFinderTests
    {
        [TestMethod]
        public void FindNeighbours_NoBorders_NoneAlive() =>
            NeighbourFinderBase.FindNeighbours_NoBorders_NoneAlive(new OpenNeighbourFinder<StandardCell, StandardCellGrid>());

        [TestMethod]
        public void FindNeighbours_NoBorders_TwoAlive() =>
            NeighbourFinderBase.FindNeighbours_NoBorders_TwoAlive(new OpenNeighbourFinder<StandardCell, StandardCellGrid>());

        [TestMethod]
        public void FindNeighbours_OnBorder_ReachesOver() =>
            NeighbourFinderBase.FindNeighbours_OnBorder(new OpenNeighbourFinder<StandardCell, StandardCellGrid>(), res => res.Count(c => c.IsAlive) == 2);
    }
}