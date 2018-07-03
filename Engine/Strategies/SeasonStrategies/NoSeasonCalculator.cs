using Engine.Entities.Environmental;

namespace Engine.Strategies.SeasonStrategies
{
    public class NoSeasonCalculator : ICalculateSeason
    {
        public SeasonalTime GetSeasonOfNextTick(SeasonalTime currentSeason) => SeasonalTime.None;
        public double CalculateTemperature(SeasonalTime season) => 0;
    }
}