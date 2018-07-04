using Engine.Entities;
using Engine.Entities.Environmental;
using Engine.Helpers;

namespace Graphics.Console
{
    public class EnvironmentalConsoleRenderer : BaseConsoleRenderer<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData>
    {
        public EnvironmentalConsoleRenderer() : base(new Match<EnvironmentalCell, char>(
            (cell => !cell.IsAlive, _ => BaseCell.DeadOut),
            (cell => cell.Diet == DietaryRestriction.Carnivore, _ => EnvironmentalCell.Carnivore),
            (cell => cell.Diet == DietaryRestriction.Herbivore, _ => EnvironmentalCell.Herbivore),
            (_ => true, _ => BaseCell.Alive)))
        {
        }
    }
}