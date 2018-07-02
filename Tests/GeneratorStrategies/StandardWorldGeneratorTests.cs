using System;
using System.Linq;
using Engine.Strategies.GeneratorStrategies;
using Engine.Entities.Standard;
using Engine.Entities.Standard.Builder;
using Engine.Helpers.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Tests.Sut.Standard;

namespace Tests.GeneratorStrategies
{
    [TestClass]
    public class StandardWorldGeneratorTests
    {
        private string GridToString(StandardCellGrid grid) =>
            string.Join(Environment.NewLine, grid.Cells.SelectMany(row => string.Join(", ", row)));

        [TestMethod]
        public void Ticks_WithOscillating_100Generations_IsSame()
        {
            // arrange
            var worldGenerator = new StandardWorldGenerator();
            var cellGrid = new StandardCellGrid
            (
                new[]
                {
                    new[] {Dead(), Dead(), Dead(), Dead()},
                    new[] {Dead(), Alive(), Alive(), Dead()},
                    new[] {Dead(), Alive(), Alive(), Dead()},
                    new[] {Dead(), Dead(), Dead(), Dead()}
                }
            );
            var world = new StandardWorldBuilder().With(w => w.Data, new StandardWorldData {Grid = cellGrid}).Create();

            // act
            var result = worldGenerator.Ticks(world).Skip(100).First().Data.Grid;

            // assert
            Assert.AreEqual(GridToString(cellGrid), GridToString(result));
        }

        [TestMethod]
        public void Ticks_OneAlive_100Generations_AllDead()
        {
            // arrange
            var worldGenerator = new StandardWorldGenerator();
            var cellGrid = new StandardCellGrid
            (
                new[]
                {
                    new[] {Dead(), Dead(), Dead()},
                    new[] {Dead(), Alive(), Dead()},
                    new[] {Dead(), Dead(), Dead()}
                }
            );
            var world = new StandardWorldBuilder().With(w => w.Data, new StandardWorldData { Grid = cellGrid }).Create();
            var expected = new StandardCellGrid(EnumerablePrelude.Repeat(() => EnumerablePrelude.Repeat(Dead, 3).ToArray(), 3).ToArray());

            // act
            var result = worldGenerator.Ticks(world).Skip(100).First().Data.Grid;

            // assert
            Assert.AreEqual(GridToString(expected), GridToString(result));
        }
    }
}