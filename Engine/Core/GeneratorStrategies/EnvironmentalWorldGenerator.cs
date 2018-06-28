using System.Collections.Generic;
using System.Linq;
using Engine.Entities.Environmental;
using Engine.Entities.Environmental.Builder;
using Engine.Helpers;

namespace Engine.Core.GeneratorStrategies
{
    public class EnvironmentalWorldGenerator : IGenerateWorld<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData, EnvironmentalWorld>
    {
        public IEnumerable<EnvironmentalWorld> Ticks(EnvironmentalWorld world)
        {
            yield return world;

            var nextGrid = world.Data.Grid.Cells.AsParallel().Select((row, outerInd) =>
                row.AsParallel().Select((_, innerInd) =>
                    world.CellCalculator.CalculateCell(
                        world.Data.Grid.Cells[outerInd][innerInd],
                        world.NeighbourFinder.FindNeighbours(world.Data.Grid, outerInd, innerInd),
                        world.Data)).ToArray()).ToArray();

            var herbivoreDensity =
                (double)world.Data.Grid.Cells.SelectMany(row => row).Where(c => c.IsAlive).Count(c => c.Diet == DietaryRestrictions.Herbivore)
                / (world.Data.Grid.Cells.Count * world.Data.Grid.Cells.Count);

            var data = new EnvironmentalWorldDataBuilder(world.Data)
                .With(wd => wd.Generation, world.Data.Generation + 1)
                .With(wd => wd.Grid, new EnvironmentalCellGrid(nextGrid))
                .With(wd => wd.HerbivoreDensity, new Density(herbivoreDensity))
                .Create();

            var nextWorld = new EnvironmentalWorldBuilder(world).With(w => w.Data, data).Create();

            foreach (var tick in Ticks(nextWorld))
            {
                yield return tick;
            }
        }
    }
}