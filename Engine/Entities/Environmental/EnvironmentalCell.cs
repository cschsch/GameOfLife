namespace Engine.Entities.Environmental
{
    public class EnvironmentalCell : BaseCell
    {
        public DietaryRestrictions Diet { get; set; }

        public static char Carnivore => 'X';
        public static char Herbivore => 'O';

        public EnvironmentalCell() { }

        public EnvironmentalCell(EnvironmentalCell other)
        {
            IsAlive = other.IsAlive;
            LifeTime = other.LifeTime;
            Diet = other.Diet;
        }
    }

    public enum DietaryRestrictions
    {
        Carnivore, Herbivore
    }
}