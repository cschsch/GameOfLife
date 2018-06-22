using System;
using System.Linq;
using GameOfLife.Core.CalculatorStrategies;
using GameOfLife.Core.GeneratorStrategies;
using GameOfLife.Core.NeighbourStrategies;
using GameOfLife.Entities;
using GameOfLife.Entities.Builder;
using GameOfLife.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TicksTests
    {
        private World World(params Cell[] cells)
        {
            var grid = new CellGrid(cells
                .Partition((int)Math.Sqrt(cells.Length))
                .Select(row => row.ToArray()).ToArray());
            var data = new WorldDataBuilder().WithGrid(grid).Create();
            var neighbourFinder = new OpenNeighbourFinder();
            var cellCalculator = new StandardCellCalculator();
            var enumerator = new StandardGenerator();
            return new WorldBuilder()
                .WithData(data)
                .WithNeighbourFinder(neighbourFinder)
                .WithCellCalculator(cellCalculator)
                .WithGenerator(enumerator)
                .Create();
        }

        private World RoundWorld(params Cell[] cells) => new WorldBuilder(World(cells)).WithNeighbourFinder(new OpenNeighbourFinder()).Create();

        private World ClosedWorld(params Cell[] cells) => new WorldBuilder(World(cells)).WithNeighbourFinder(new ClosedNeighbourFinder()).Create();

        private Cell C(string representation) => representation == " " ? new Cell { IsAlive = false, LifeTime = 0 } : new Cell { IsAlive = true, LifeTime = 1 };

        [TestMethod]
        public void FrameAfterTickIsExpected()
        {
            var pairs = new[]
            {
                (ClosedWorld(C(" "), C(" "), C(" "), C(" "), C(" "),
                             C(" "), C(" "), C("X"), C(" "), C(" "),
                             C(" "), C(" "), C("X"), C(" "), C(" "),
                             C(" "), C(" "), C("X"), C(" "), C(" "),
                             C(" "), C(" "), C(" "), C(" "), C(" ")),

                 ClosedWorld(C(" "), C(" "), C(" "), C(" "), C(" "),
                             C(" "), C(" "), C(" "), C(" "), C(" "),
                             C(" "), C("X"), C("X"), C("X"), C(" "),
                             C(" "), C(" "), C(" "), C(" "), C(" "),
                             C(" "), C(" "), C(" "), C(" "), C(" "))),

                 (RoundWorld(C(" "), C(" "), C(" "), C(" "), C(" "),
                             C(" "), C(" "), C(" "), C(" "), C("X"),
                             C(" "), C(" "), C(" "), C(" "), C("X"),
                             C(" "), C(" "), C(" "), C(" "), C("X"),
                             C(" "), C(" "), C(" "), C(" "), C(" ")),

                  RoundWorld(C(" "), C(" "), C(" "), C(" "), C(" "),
                             C(" "), C(" "), C(" "), C(" "), C(" "),
                             C("X"), C(" "), C(" "), C("X"), C("X"),
                             C(" "), C(" "), C(" "), C(" "), C(" "),
                             C(" "), C(" "), C(" "), C(" "), C(" "))),

                 (ClosedWorld(C(" "), C(" "), C(" "), C(" "),
                              C(" "), C(" "), C("X"), C(" "),
                              C(" "), C("X"), C("X"), C(" "),
                              C(" "), C(" "), C(" "), C(" ")),

                  ClosedWorld(C(" "), C(" "), C(" "), C(" "),
                              C(" "), C("X"), C("X"), C(" "),
                              C(" "), C("X"), C("X"), C(" "),
                              C(" "), C(" "), C(" "), C(" ")))
            };

            foreach (var (input, expected) in pairs)
            {
                var result = input.Ticks().Skip(1).First();
                Assert.AreEqual(expected.ToString(), result.ToString());
            }
        }

        [TestMethod]
        public void LifetimeAfterTicksIsExpected()
        {
            var world = ClosedWorld(
                C(" "), C(" "), C(" "), C(" "),
                C(" "), C("X"), C("X"), C(" "),
                C(" "), C("X"), C("X"), C(" "),
                C(" "), C(" "), C(" "), C(" "));
            const int expected = 101;

            var result = world.Ticks().Skip(100).First().Data.Grid.Cells[1][1].LifeTime;

            Assert.AreEqual(expected, result);
        }
    }
}
