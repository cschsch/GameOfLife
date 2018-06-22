using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using GameOfLife.Core.Calculators;
using GameOfLife.Core.Enumerators;
using GameOfLife.Core.Neighbours;
using Tp.Core;

namespace GameOfLife.Core.Worlds
{
    public class World
    {
        public WorldData Data { get; set; }
        public IFindNeighbours NeighbourFinder { get; set; }
        public ICalculateCell CellCalculator { get; set; }
        public IEnumerateWorld Enumerator { get; set; }

        public IEnumerable<World> Ticks() => Enumerator.Ticks(this);
    }
}