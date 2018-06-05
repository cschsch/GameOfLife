using System;
using System.Collections.Immutable;
using System.Linq;
using GameOfLife.Core;
using GameOfLife.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TicksTests
    {
        private World World(params Cell[] cells) =>
            new World().WithCells(ImmutableArray.CreateRange(cells
                .Partition((int) Math.Sqrt(cells.Length))
                .Select(ImmutableArray.CreateRange)));

        private World RoundWorld(params Cell[] cells) => World(cells).WithNeighbour(new Open());

        private World ClosedWorld(params Cell[] cells) => World(cells).WithNeighbour(new Closed());

        private Cell C(string representation) => representation == " " ? new Cell(false, 0) : new Cell(true, 1);

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

            var result = world.Ticks().Skip(100).First().Cells[1][1].LifeTime;

            Assert.AreEqual(expected, result);
        }
    }
}
