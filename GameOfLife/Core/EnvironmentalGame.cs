using GameOfLife.Entities.Environmental;
using GameOfLife.Renderer;

namespace GameOfLife.Core
{
    public class EnvironmentalGame : BaseGame<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData, EnvironmentalWorld>
    {
        public EnvironmentalGame(IRenderer<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData> renderer, IAnalyzeResults<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData> resultAnalyzer) : base(renderer, resultAnalyzer)
        {
        }
    }
}