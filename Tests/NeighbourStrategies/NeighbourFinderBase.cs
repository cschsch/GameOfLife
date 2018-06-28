using System;
using System.Collections.Generic;
using System.Linq;
using GameOfLife.Core.NeighbourStrategies;
using GameOfLife.Entities.Standard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Tests.Sut.Standard;

namespace Tests.NeighbourStrategies
{
    public static class NeighbourFinderBase
    {
        public static void FindNeighbours_NoBorders_NoneAlive(IFindNeighbours<StandardCell, StandardCellGrid> neighbourFinder)
        {
            // arrange
            var cells = new []
            {
                new[] {Dead(), Dead(), Dead(), Dead(), Dead()},
                new[] {Dead(), Dead(), Dead(), Dead(), Dead()},
                new[] {Dead(), Dead(), Alive(), Dead(), Dead()},
                new[] {Dead(), Dead(), Dead(), Dead(), Dead()},
                new[] {Dead(), Dead(), Dead(), Dead(), Dead()}
            };
            var (outerIndex, innerIndex) = (2, 2);

            // act
            var result = neighbourFinder.FindNeighbours(new StandardCellGrid(cells), outerIndex, innerIndex);

            // assert
            Assert.IsFalse(result.Any(c => c.IsAlive));
        }

        public static void FindNeighbours_NoBorders_TwoAlive(IFindNeighbours<StandardCell, StandardCellGrid> neighbourFinder)
        {
            // arrange
            var cells = new[]
            {
                new[] {Dead(), Dead(), Dead(), Dead(), Dead()},
                new[] {Dead(), Alive(), Dead(), Dead(), Dead()},
                new[] {Dead(), Dead(), Alive(), Dead(), Dead()},
                new[] {Dead(), Dead(), Alive(), Dead(), Dead()},
                new[] {Dead(), Dead(), Dead(), Dead(), Dead()}
            };
            var (outerIndex, innerIndex) = (2, 2);

            // act
            var result = neighbourFinder.FindNeighbours(new StandardCellGrid(cells), outerIndex, innerIndex);

            // assert
            Assert.IsTrue(result.Count(c => c.IsAlive) == 2);
        }

        public static void FindNeighbours_OnBorder(IFindNeighbours<StandardCell, StandardCellGrid> neighbourFinder, Func<IEnumerable<StandardCell>, bool> assertion)
        {
            // arrange
            var cells = new[]
            {
                new[] {Dead(), Dead(), Dead(), Dead(), Dead()},
                new[] {Alive(), Dead(), Dead(), Dead(), Dead()},
                new[] {Alive(), Dead(), Dead(), Dead(), Alive()},
                new[] {Dead(), Dead(), Dead(), Dead(), Dead()},
                new[] {Dead(), Dead(), Dead(), Dead(), Dead()}
            };
            var (outerIndex, innerIndex) = (2, 4);

            // act
            var result = neighbourFinder.FindNeighbours(new StandardCellGrid(cells), outerIndex, innerIndex);

            // assert
            Assert.IsTrue(assertion(result));
        }
    }
}