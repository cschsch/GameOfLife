namespace GameOfLife.Entities
{
    public class Cell
    {
        public bool IsAlive { get; set; }
        public int LifeTime { get; set; }
        public DietaryRestrictions Diet { get; set; }
    }

    public enum DietaryRestrictions
    {
        Carnivore, Herbivore
    }

    public static class CellExtensions
    {
        public static Cell ToAlive(this Cell cell)
        {
            return new Cell {IsAlive = true, LifeTime = 1, Diet = cell.Diet};
        }

        public static Cell Kill(this Cell cell)
        {
            return new Cell {IsAlive = false, LifeTime = 0, Diet = cell.Diet};
        }

        public static Cell IncrementLifetime(this Cell cell)
        {
            return new Cell {IsAlive = cell.IsAlive, LifeTime = cell.LifeTime + 1, Diet = cell.Diet};
        }
    }
}