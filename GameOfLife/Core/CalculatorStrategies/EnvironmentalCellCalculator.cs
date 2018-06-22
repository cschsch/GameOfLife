using System.Collections.Generic;
using GameOfLife.Entities;

namespace GameOfLife.Core.CalculatorStrategies
{
    public class EnvironmentalCellCalculator : ICalculateCell
    {
        public Cell CalculateCell(Cell cell, IEnumerable<Cell> neighbours, WorldData data)
        {
            throw new System.NotImplementedException();
        }
    }
}