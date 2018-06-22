using System.Collections.Generic;
using GameOfLife.Entities;

namespace GameOfLife.Core.CalculatorStrategies
{
    public interface ICalculateCell
    {
        Cell CalculateCell(Cell cell, IEnumerable<Cell> neighbours, WorldData data);
    }
}