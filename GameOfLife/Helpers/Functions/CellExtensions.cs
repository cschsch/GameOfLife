using GameOfLife.Entities;
using GameOfLife.Entities.Builder;

namespace GameOfLife.Helpers.Functions
{
    public static class CellExtensions
    {
        public static Cell ToAlive(this Cell cell) => new CellBuilder(cell).WithAlive(true).WithLifetime(1).Create();
        public static Cell Kill(this Cell cell) => new CellBuilder(cell).WithAlive(false).WithLifetime(0).Create();
        public static Cell IncrementLifetime(this Cell cell) => new CellBuilder(cell).WithLifetime(cell.LifeTime + 1).Create();
    }
}