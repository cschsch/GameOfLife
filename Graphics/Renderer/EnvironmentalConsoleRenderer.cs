using System.Collections.Generic;
using Engine.Entities;
using Engine.Entities.Environmental;
using Engine.Helpers;

namespace Graphics.Renderer
{
    public class EnvironmentalConsoleRenderer : BaseConsoleRenderer<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData>
    {
        public EnvironmentalConsoleRenderer() : base(new Match<EnvironmentalCell, char>(
            (cell => !cell.IsAlive, _ => BaseCell.DeadOut),
            (cell => cell.Diet == DietaryRestrictions.Carnivore, _ => EnvironmentalCell.Carnivore),
            (cell => cell.Diet == DietaryRestrictions.Herbivore, _ => EnvironmentalCell.Herbivore)))
        {
        }
    }
}