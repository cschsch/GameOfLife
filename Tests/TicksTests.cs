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
        private IWorld RoundWorld(params Cell[] cells) =>
            new RoundWorld(cells.Partition((int) Math.Sqrt(cells.Length)).Select(row => row.ToImmutableArray())
                .ToImmutableArray());

        private IWorld ClosedWorld(params Cell[] cells) =>
            new ClosedWorld(cells.Partition((int)Math.Sqrt(cells.Length)).Select(row => row.ToImmutableArray())
                .ToImmutableArray());

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
    }
}
