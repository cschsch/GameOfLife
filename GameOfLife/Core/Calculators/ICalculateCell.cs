using System.Collections.Generic;
using GameOfLife.Core.Worlds;

namespace GameOfLife.Core.Calculators
{
    public interface ICalculateCell
    {
        Cell CalculateCell(Cell cell, IEnumerable<Cell> neighbours, WorldData data);
    }
}