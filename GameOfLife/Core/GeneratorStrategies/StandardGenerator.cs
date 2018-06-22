using System.Collections.Generic;
using System.Linq;
using GameOfLife.Entities;
using GameOfLife.Entities.Builder;

namespace GameOfLife.Core.GeneratorStrategies
{
    public class StandardGenerator : IGenerateWorld
    {
        public IEnumerable<World> Ticks(World world)
        {
            yield return world;

            var nextGrid = world.Data.Grid.Cells.AsParallel().Select((row, outerInd) =>
                row.AsParallel().Select((_, innerInd) =>
                    world.CellCalculator.CalculateCell(
                        world.Data.Grid.Cells[outerInd][innerInd],
                        world.NeighbourFinder.FindNeighbours(world.Data.Grid.Cells, outerInd, innerInd),
                        world.Data)).ToArray()).ToArray();

            var herbivoreDensity =
                (double) world.Data.Grid.Cells.SelectMany(row => row).Count(c => c.Diet == DietaryRestrictions.Herbivore)
                / (world.Data.Grid.Cells.Count * world.Data.Grid.Cells.Count);

            var data = new WorldDataBuilder(world.Data)
                .WithGrid(new CellGrid(nextGrid))
                .WithHerbivoreDensity(herbivoreDensity)
                .WithTemperature(world.Data.Temperature)
                .Create();

            var nextWorld = new WorldBuilder(world).WithData(data).Create();

            foreach (var tick in world.Generator.Ticks(nextWorld))
            {
                yield return tick;
            }
        }
    }
}