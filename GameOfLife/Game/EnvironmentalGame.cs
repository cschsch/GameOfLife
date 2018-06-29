using Engine.Entities.Environmental;
using Graphics.Renderer;
using GameOfLife.ResultAnalyzer;

namespace GameOfLife.Game
{
    public class EnvironmentalGame : BaseGame<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData, EnvironmentalWorld>
    {
        public EnvironmentalGame(IRenderer<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData> renderer, IAnalyzeResults<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData> resultAnalyzer) : base(renderer, resultAnalyzer)
        {
        }
    }
}