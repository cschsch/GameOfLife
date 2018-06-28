using GameOfLife.Entities;
using GameOfLife.Entities.Environmental;
using GameOfLife.Entities.Environmental.Builder;
using GameOfLife.Entities.Standard;
using GameOfLife.Entities.Standard.Builder;
using GameOfLife.Helpers;

namespace Tests
{
    public static class Sut
    {
        private static TCell Dead<TCell, TBuilder>()
            where TCell : BaseCell
            where TBuilder : GenericBuilder<TCell>, new()
            => new TBuilder().With(c => c.IsAlive, false).With(c => c.LifeTime, 0).Create();

        private static TCell Alive<TCell, TBuilder>()
            where TCell : BaseCell
            where TBuilder : GenericBuilder<TCell>, new()
            => new TBuilder().With(c => c.IsAlive, true).With(c => c.LifeTime, 1).Create();

        public static class Standard
        {
            public static StandardCell Dead() => Dead<StandardCell, StandardCellBuilder>();
            public static StandardCell Alive() => Alive<StandardCell, StandardCellBuilder>();
        }

        public static class Environmental
        {
            public static EnvironmentalCell Dead() => Dead<EnvironmentalCell, EnvironmentalCellBuilder>();
            public static EnvironmentalCell Alive() => Alive<EnvironmentalCell, EnvironmentalCellBuilder>();
        }
    }
}