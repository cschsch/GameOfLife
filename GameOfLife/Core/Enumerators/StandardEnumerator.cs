﻿using System.Collections.Generic;
using System.Linq;
using GameOfLife.Core.Worlds;

namespace GameOfLife.Core.Enumerators
{
    public class StandardEnumerator : IEnumerateWorld
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

            var nextWorld = new WorldBuilder(world).WithData(new WorldData {Grid = new CellGrid(nextGrid)}).Create();

            foreach (var tick in world.Enumerator.Ticks(nextWorld))
            {
                yield return tick;
            }
        }
    }
}