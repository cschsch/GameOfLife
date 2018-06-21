using System.Collections.Generic;

namespace GameOfLife.Core.Calculators
{
    public interface ICalculateCell
    {
        Cell CalculateCell(Cell cell, IEnumerable<Cell> neighbours);
    }
}