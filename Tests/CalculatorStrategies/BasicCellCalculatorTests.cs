using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife.Core.CalculatorStrategies;
using GameOfLife.Entities;

using static Tests.Sut;

namespace Tests.CalculatorStrategies
{
    [TestClass]
    public class BasicCellCalculatorTests
    {
        [TestMethod]
        public void CalculateCell_IsAlive_NoneAlive_Dies() =>
            CellCalculatorBase.CalculateCell(new BasicCellCalculator(), Alive(), Enumerable.Empty<Cell>(), new WorldData(),
                cell => !cell.IsAlive,
                cell => cell.LifeTime == 0);

        [TestMethod]
        public void CalculateCell_IsAlive_TwoAlive_StaysAlive() =>
            CellCalculatorBase.CalculateCell(new BasicCellCalculator(), Alive(), Enumerable.Repeat(Alive(), 2), new WorldData(),
                cell => cell.IsAlive,
                cell => cell.LifeTime == 2);

        [TestMethod]
        public void CalculateCell_IsAlive_FiveAlive_Dies() =>
            CellCalculatorBase.CalculateCell(new BasicCellCalculator(), Alive(), Enumerable.Repeat(Alive(), 5), new WorldData(),
                cell => !cell.IsAlive,
                cell => cell.LifeTime == 0);

        [TestMethod]
        public void CalculateCell_IsDead_ThreeAlive_ComesAlive() =>
            CellCalculatorBase.CalculateCell(new BasicCellCalculator(), Dead(), Enumerable.Repeat(Alive(), 3), new WorldData(),
                cell => cell.IsAlive,
                cell => cell.LifeTime == 1);
    }
}