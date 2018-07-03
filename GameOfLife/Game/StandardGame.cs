using Engine.Entities.Standard;

using Graphics.Console;

using ResultAnalyzer;

namespace GameOfLife.Game
{
    public class StandardGame : BaseGame<StandardCell, StandardCellGrid, StandardWorldData, StandardWorld>
    {
        public StandardGame(IRenderer<StandardCell, StandardCellGrid, StandardWorldData> renderer, IAnalyzeResults<StandardCell, StandardCellGrid, StandardWorldData> resultAnalyzer) : base(renderer, resultAnalyzer)
        {
        }
    }
}