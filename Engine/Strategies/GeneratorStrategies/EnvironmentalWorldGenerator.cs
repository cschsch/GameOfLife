using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engine.Entities.Environmental;
using Engine.Entities.Environmental.Builders;
using Engine.Helpers;

namespace Engine.Strategies.GeneratorStrategies
{
    public class EnvironmentalWorldGenerator : IGenerateWorld<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData, EnvironmentalWorld>
    {
        public IEnumerable<EnvironmentalWorld> Ticks(EnvironmentalWorld world)
        {
            yield return world;

            var nextGrid = Task.Run(() => world.Data.Grid.Cells.AsParallel().Select((row, outerInd) =>
                row.AsParallel().Select((_, innerInd) =>
                    world.CellCalculator.CalculateCell(
                        world.Data.Grid.Cells[outerInd][innerInd],
                        world.NeighbourFinder.FindNeighbours(world.Data.Grid, outerInd, innerInd),
                        world.Data)).ToArray()).ToArray());

            var herbivoreDensity = Task.Run(() =>
                (double)world.Data.Grid.Cells.SelectMany(row => row).Where(c => c.IsAlive).Count(c => c.Diet == DietaryRestriction.Herbivore)
                / (world.Data.Grid.Cells.Count * world.Data.Grid.Cells.Count));

            var nextSeason = Task.Run(() => world.SeasonCalculator.GetSeasonOfNextTick(world.Data.Season));
            var nextTemperature = Task.Run(() => world.SeasonCalculator.CalculateTemperature(world.Data.Season));

            var data = new EnvironmentalWorldData
            {
                Generation = world.Data.Generation + 1,
                Grid = new EnvironmentalCellGrid(nextGrid.Result),
                HerbivoreDensity = new Density(herbivoreDensity.Result),
                Season = nextSeason.Result,
                Temperature = nextTemperature.Result
            };

            var nextWorld = new EnvironmentalWorldBuilder(world).With(w => w.Data, data).Create();

            foreach (var tick in Ticks(nextWorld))
            {
                yield return tick;
            }
        }
    }
}