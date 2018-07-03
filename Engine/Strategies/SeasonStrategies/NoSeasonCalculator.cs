using Engine.Entities.Environmental;

namespace Engine.Strategies.SeasonStrategies
{
    public class NoSeasonCalculator : ICalculateSeason
    {
        public Season GetSeasonOfNextTick(Season currentSeason) => default(Season);
        public double CalculateTemperature(Season season) => 0;
    }
}