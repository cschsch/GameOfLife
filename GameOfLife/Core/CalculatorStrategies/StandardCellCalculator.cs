using System.Collections.Generic;
using System.Linq;
using GameOfLife.Entities.Standard;
using GameOfLife.Entities.Standard.Builder;
using GameOfLife.Helpers;

namespace GameOfLife.Core.CalculatorStrategies
{
    public class StandardCellCalculator : ICalculateCell<StandardCell, StandardCellGrid, StandardWorldData>
    {
        public StandardCell CalculateCell(StandardCell cell, IEnumerable<StandardCell> neighbours, StandardWorldData data)
        {
            var alive = neighbours.Count(n => n.IsAlive);

            return new Match<StandardCell, StandardCell>(
                    (cMatch => !cMatch.IsAlive && alive == 3, cMatch => new StandardCellBuilder(cMatch).With(c => c.IsAlive, true).With(c => c.LifeTime, 1).Create()),
                    (_ => alive < 2 || alive > 3, cMatch => new StandardCellBuilder(cMatch).With(c => c.IsAlive, false).With(c => c.LifeTime, 0).Create()),
                    (_ => true, cMatch => new StandardCellBuilder(cMatch).With(c => c.LifeTime, cMatch.LifeTime + 1).Create()))
                .MatchFirst(cell);
        }
    }
}