using System.Collections.Generic;
using GameOfLife.Core.Worlds;

namespace GameOfLife.Core.Calculators
{
    public class EnvironmentalCellCalculator : ICalculateCell
    {
        public Cell CalculateCell(Cell cell, IEnumerable<Cell> neighbours, WorldData data)
        {
            throw new System.NotImplementedException();
        }
    }
}