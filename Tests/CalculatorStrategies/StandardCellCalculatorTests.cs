using System.Linq;
using Engine.Strategies.CalculatorStrategies;
using Engine.Entities.Standard;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Tests.Sut.Standard;

namespace Tests.CalculatorStrategies
{
    [TestClass]
    public class StandardCellCalculatorTests
    {
        [TestMethod]
        public void CalculateCell_IsAlive_NoneAlive_Dies() =>
            CellCalculatorBase.CalculateCell(new StandardCellCalculator(), Alive(), Enumerable.Empty<StandardCell>(), new StandardWorldData(),
                cell => !cell.IsAlive,
                cell => cell.LifeTime == 0);

        [TestMethod]
        public void CalculateCell_IsAlive_TwoAlive_StaysAlive() =>
            CellCalculatorBase.CalculateCell(new StandardCellCalculator(), Alive(), Enumerable.Repeat(Alive(), 2), new StandardWorldData(),
                cell => cell.IsAlive,
                cell => cell.LifeTime == 2);

        [TestMethod]
        public void CalculateCell_IsAlive_FiveAlive_Dies() =>
            CellCalculatorBase.CalculateCell(new StandardCellCalculator(), Alive(), Enumerable.Repeat(Alive(), 5), new StandardWorldData(),
                cell => !cell.IsAlive,
                cell => cell.LifeTime == 0);

        [TestMethod]
        public void CalculateCell_IsDead_ThreeAlive_ComesAlive() =>
            CellCalculatorBase.CalculateCell(new StandardCellCalculator(), Dead(), Enumerable.Repeat(Alive(), 3), new StandardWorldData(),
                cell => cell.IsAlive,
                cell => cell.LifeTime == 1);
    }
}