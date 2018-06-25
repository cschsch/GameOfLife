using GameOfLife.Entities;
using GameOfLife.Entities.Builder;

namespace Tests
{
    public static class Sut
    {
        public static CellBuilder CellBuilder() => new CellBuilder();
        public static WorldDataBuilder WorldDataBuilder() => new WorldDataBuilder();

        public static Cell Dead() => CellBuilder().WithAlive(false).WithLifetime(0).Create();
        public static Cell Alive() => CellBuilder().WithAlive(true).WithLifetime(1).Create();
    }
}