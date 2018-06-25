using System;
using System.Collections.Generic;
using GameOfLife.Core.CalculatorStrategies;
using GameOfLife.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.CalculatorStrategies
{
    public static class CellCalculatorBase
    {
        public static void CalculateCell(ICalculateCell cellCalculator, Cell cell, IEnumerable<Cell> neighbours, WorldData data, params Func<Cell, bool>[] assertions)
        {
            var result = cellCalculator.CalculateCell(cell, neighbours, data);
            foreach (var assertion in assertions)
            {
                Assert.IsTrue(assertion(result));
            }
        }
    }
}