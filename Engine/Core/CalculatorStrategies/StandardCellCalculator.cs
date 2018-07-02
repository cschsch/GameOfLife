using System.Collections.Generic;
using System.Linq;
using Engine.Entities.Standard;
using Engine.Entities.Standard.Builder;
using Engine.Helpers;

namespace Engine.Core.CalculatorStrategies
{
    public class StandardCellCalculator : ICalculateCell<StandardCell, StandardCellGrid, StandardWorldData>
    {
        public StandardCell CalculateCell(StandardCell cell, IEnumerable<StandardCell> neighbours, StandardWorldData data)
        {
            var alive = neighbours.Count(n => n.IsAlive);

            return new Match<StandardCell, StandardCell>(
                    (cMatch => !cMatch.IsAlive && alive == 3, _ => new StandardCell {IsAlive = true, LifeTime = 1}),
                    (_ => alive < 2 || alive > 3, _ => new StandardCell {IsAlive = false, LifeTime = 0}),
                    (_ => true, cMatch => new StandardCell(cMatch) {LifeTime = cMatch.LifeTime + 1}))
                .MatchFirst(cell);
        }
    }
}