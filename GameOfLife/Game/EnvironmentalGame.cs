using Engine.Entities.Environmental;

using GameOfLife.ResultAnalyzer;

using Graphics.Renderer;

namespace GameOfLife.Game
{
    public class EnvironmentalGame : BaseGame<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData, EnvironmentalWorld>
    {
        public EnvironmentalGame(IRenderer<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData> renderer, IAnalyzeResults<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData> resultAnalyzer) : base(renderer, resultAnalyzer)
        {
        }
    }
}