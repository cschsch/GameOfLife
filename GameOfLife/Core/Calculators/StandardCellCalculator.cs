using System.Collections.Generic;
using System.Linq;
using GameOfLife.Helpers;

namespace GameOfLife.Core.Calculators
{
    public class StandardCellCalculator : ICalculateCell
    {
        public Cell CalculateCell(Cell cell, IEnumerable<Cell> neighbours)
        {
            var alive = neighbours.Count(n => n.IsAlive);

            return new Match<Cell, Cell>(
                    (c => !c.IsAlive && alive == 3, _ => new Cell(true, 1)),
                    (_ => alive < 2 || alive > 3, _ => new Cell(false, 0)),
                    (_ => true, c => new Cell(c.IsAlive, c.LifeTime + 1)))
                .MatchFirst(cell);
        }
    }
}