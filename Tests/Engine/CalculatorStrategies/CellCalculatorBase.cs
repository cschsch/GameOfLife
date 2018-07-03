using System;
using System.Collections.Generic;
using Engine.Entities;
using Engine.Strategies.CalculatorStrategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Engine.CalculatorStrategies
{
    public static class CellCalculatorBase
    {
        public static void CalculateCell<TCell, TCellGrid, TWorldData>(
            ICalculateCell<TCell, TCellGrid, TWorldData> cellCalculator, TCell cell, IEnumerable<TCell> neighbours,
            TWorldData data, params Func<TCell, bool>[] assertions)
            where TCell : BaseCell
            where TCellGrid : BaseCellGrid<TCell>
            where TWorldData : BaseWorldData<TCell, TCellGrid>
        {
            var result = cellCalculator.CalculateCell(cell, neighbours, data);
            foreach (var assertion in assertions)
            {
                Assert.IsTrue(assertion(result));
            }
        }
    }
}