﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using GameOfLife.Helpers;

namespace GameOfLife.Core
{
    public class Closed : IGetNeighbours
    {
        public IEnumerable<Cell> GetNeighbours(ImmutableArray<ImmutableArray<Cell>> cells, int outerIndex, int innerIndex) =>
            cells.GetValuesSafe(Enumerable.Range(outerIndex - 1, 3))
                .SelectMany(row => row.GetValuesSafe(Enumerable.Range(innerIndex - 1, 3))).Except(new[] {cells[outerIndex][innerIndex]});
    }
}