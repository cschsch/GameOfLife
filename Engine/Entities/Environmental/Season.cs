namespace Engine.Entities.Environmental
{
    public class Season
    {
        public Seasons Id { get; set; }
        public int Length { get; set; }
        public int CurrentTime { get; set; }

        public Season() { }

        public Season(Season other)
        {
            Id = other.Id;
            Length = other.Length;
            CurrentTime = other.CurrentTime;
        }

        public static Season Spring => new Season {Id = Seasons.Spring, CurrentTime = 0, Length = 26};
        public static Season Summer => new Season { Id = Seasons.Summer, CurrentTime = 0, Length = 29 };
        public static Season Autumn => new Season { Id = Seasons.Autumn, CurrentTime = 0, Length = 27 };
        public static Season Winter => new Season { Id = Seasons.Winter, CurrentTime = 0, Length = 31 };
    }

    public enum Seasons
    {
        Spring, Summer, Autumn, Winter
    }
}