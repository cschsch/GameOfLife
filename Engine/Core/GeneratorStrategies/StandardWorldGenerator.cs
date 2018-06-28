using System.Collections.Generic;
using System.Linq;
using Engine.Entities.Standard;
using Engine.Entities.Standard.Builder;

namespace Engine.Core.GeneratorStrategies
{
    public class StandardWorldGenerator : IGenerateWorld<StandardCell, StandardCellGrid, StandardWorldData, StandardWorld>
    {
        public IEnumerable<StandardWorld> Ticks(StandardWorld world)
        {
            yield return world;

            var nextGrid = world.Data.Grid.Cells.AsParallel().Select((row, outerInd) =>
                row.AsParallel().Select((_, innerInd) =>
                    world.CellCalculator.CalculateCell(
                        world.Data.Grid.Cells[outerInd][innerInd],
                        world.NeighbourFinder.FindNeighbours(world.Data.Grid, outerInd, innerInd),
                        world.Data)).ToArray()).ToArray();

            var data = new StandardWorldDataBuilder(world.Data)
                .With(wd => wd.Generation, world.Data.Generation + 1)
                .With(wd => wd.Grid, new StandardCellGrid(nextGrid))
                .Create();

            var nextWorld = new StandardWorldBuilder(world).With(w => w.Data, data).Create();

            foreach (var tick in Ticks(nextWorld))
            {
                yield return tick;
            }
        }
    }
}