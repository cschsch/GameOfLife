using System.Collections.Generic;
using System.Collections.Immutable;

namespace GameOfLife.Core
{
    public interface IWorld
    {
        ImmutableArray<ImmutableArray<Cell>> Cells { get; }
        IEnumerable<IWorld> Ticks();
    }
}