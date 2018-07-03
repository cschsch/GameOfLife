using System;
using System.Linq;

using Engine.Entities.Environmental;
using Engine.Entities.Environmental.Builders;
using Engine.Helpers.Functions;
using Engine.Strategies.GeneratorStrategies;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Tests.Sut.Environmental;

namespace Tests.Engine.GeneratorStrategies
{
    [TestClass]
    public class EnvironmentalWorldGeneratorTests
    {
        private string GridToString(EnvironmentalCellGrid grid) =>
            string.Join(Environment.NewLine, grid.Cells.SelectMany(row => string.Join(", ", row)));

        [TestMethod]
        public void Ticks_WithOscillating_100Generations_IsSame()
        {
            // arrange
            var worldGenerator = new EnvironmentalWorldGenerator();
            var cellGrid = new EnvironmentalCellGrid
            (
                new[]
                {
                    new[] {Dead(), Dead(),  Dead(), Dead()},
                    new[] {Dead(), Alive(), Alive(), Dead()},
                    new[] {Dead(), Alive(), Alive(), Dead()},
                    new[] {Dead(), Dead(),  Dead(), Dead()}
                }
            );
            var world = new EnvironmentalWorldBuilder().With(w => w.Data, new EnvironmentalWorldData { Grid = cellGrid }).Create();

            // act
            var result = worldGenerator.Ticks(world).Skip(100).First().Data.Grid;

            // assert
            Assert.AreEqual(GridToString(cellGrid), GridToString(result));
        }

        [TestMethod]
        public void Ticks_OneAlive_100Generations_AllDead()
        {
            // arrange
            var worldGenerator = new EnvironmentalWorldGenerator();
            var cellGrid = new EnvironmentalCellGrid
            (
                new[]
                {
                    new[] {Dead(), Dead(), Dead()},
                    new[] {Dead(), Alive(), Dead()},
                    new[] {Dead(), Dead(), Dead()}
                }
            );
            var world = new EnvironmentalWorldBuilder().With(w => w.Data, new EnvironmentalWorldData { Grid = cellGrid }).Create();
            var expected = new EnvironmentalCellGrid(EnumerablePrelude.Repeat(() => EnumerablePrelude.Repeat(Dead, 3).ToArray(), 3).ToArray());

            // act
            var result = worldGenerator.Ticks(world).Skip(100).First().Data.Grid;

            // assert
            Assert.AreEqual(GridToString(expected), GridToString(result));
        }
    }
}