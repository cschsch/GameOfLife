namespace Engine.Entities.Environmental
{
    public class EnvironmentalCell : BaseCell
    {
        public DietaryRestrictions Diet { get; set; }

        public static char Carnivore => 'X';
        public static char Herbivore => 'O';
    }

    public enum DietaryRestrictions
    {
        Carnivore, Herbivore
    }
}