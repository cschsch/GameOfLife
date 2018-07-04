using System.Collections.Generic;
using System.Linq;

using Engine.Entities.Standard;
using Engine.Entities.Standard.Builders;
using Engine.Helpers;

namespace Engine.Strategies.CalculatorStrategies
{
    public class StandardCellCalculator : ICalculateCell<StandardCell, StandardCellGrid, StandardWorldData>
    {
        public StandardCell CalculateCell(StandardCell cell, IEnumerable<StandardCell> neighbours, StandardWorldData data)
        {
            var alive = neighbours.Count(n => n.IsAlive);

            return new Match<StandardCell, StandardCell>(
                    (cMatch => !cMatch.IsAlive && alive == 3, _ => StandardFlyweightCellFactory.GetStandardCell("Alive")),
                    (_ => alive < 2 || alive > 3, _ => StandardFlyweightCellFactory.GetStandardCell("Dead")),
                    (_ => true, cMatch => new StandardCell(cMatch) {LifeTime = cMatch.LifeTime + 1}))
                .MatchFirst(cell);
        }
    }
}