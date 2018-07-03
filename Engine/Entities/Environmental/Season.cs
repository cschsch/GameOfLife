namespace Engine.Entities.Environmental
{
    public class SeasonalTime
    {
        public Season Id { get; set; }
        public int Length { get; set; }
        public int CurrentTime { get; set; }

        public SeasonalTime() { }

        public SeasonalTime(SeasonalTime other)
        {
            Id = other.Id;
            Length = other.Length;
            CurrentTime = other.CurrentTime;
        }

        public static SeasonalTime None => new SeasonalTime {Id = Season.None, CurrentTime = 0, Length = 0};
        public static SeasonalTime Spring => new SeasonalTime {Id = Season.Spring, CurrentTime = 0, Length = 26};
        public static SeasonalTime Summer => new SeasonalTime {Id = Season.Summer, CurrentTime = 0, Length = 29};
        public static SeasonalTime Autumn => new SeasonalTime {Id = Season.Autumn, CurrentTime = 0, Length = 27};
        public static SeasonalTime Winter => new SeasonalTime {Id = Season.Winter, CurrentTime = 0, Length = 31};
    }

    public enum Season
    {
        None, Spring, Summer, Autumn, Winter
    }
}