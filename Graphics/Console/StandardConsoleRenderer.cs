﻿using Engine.Entities;
using Engine.Entities.Standard;
using Engine.Helpers;

namespace Graphics.Console
{
    public class StandardConsoleRenderer : BaseConsoleRenderer<StandardCell, StandardCellGrid, StandardWorldData>
    {
        public StandardConsoleRenderer() : base(new Match<StandardCell, char>(
            (cell => !cell.IsAlive, _ => BaseCell.DeadOut),
            (cell => cell.IsAlive, _ => BaseCell.Alive)))
        {
        }
    }
}