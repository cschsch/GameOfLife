using System.Collections.Generic;

using Engine.Entities.Environmental;
using Engine.Helpers;

namespace Engine.Strategies.SeasonStrategies
{
    public class StandardSeasonCalculator : ICalculateSeason
    {
        private readonly IReadOnlyDictionary<Season, SeasonalTime> _seasonChanger = new Dictionary<Season, SeasonalTime>
        {
            {Season.None, SeasonalTime.None},
            {Season.Spring, SeasonalTime.Summer},
            {Season.Summer, SeasonalTime.Autumn},
            {Season.Autumn, SeasonalTime.Winter},
            {Season.Winter, SeasonalTime.Spring}
        };

        public SeasonalTime GetSeasonOfNextTick(SeasonalTime currentSeason) =>
            currentSeason.CurrentTime >= currentSeason.Length
                ? _seasonChanger[currentSeason.Id]
                : new SeasonalTime(currentSeason) {CurrentTime = currentSeason.CurrentTime + 1};

        // these calculations have been chosen more or less deliberately to kind of simulate a shift in seasons regarding temperature
        // they are not meant to represent realistic values
        public double CalculateTemperature(SeasonalTime season) => new Match<SeasonalTime, double>(
                (s => s.Id == Season.Spring, s => (s.CurrentTime * 3) - s.Length), // linear ascending
                (s => s.Id == Season.Summer, s => (-0.6 * s.CurrentTime * s.CurrentTime) + (s.Length / 2 * s.CurrentTime) + (s.Length * 2)), // parabola facing down
                (s => s.Id == Season.Autumn, s => (-s.CurrentTime * 3) + s.Length), // linear descending
                (s => s.Id == Season.Winter, s => (0.6 * s.CurrentTime * s.CurrentTime) - (s.Length / 2 * s.CurrentTime) - (s.Length * 2)), // parabola facing up
                (s => s.Id == Season.None, _ => 0))
            .MatchFirst(season);
    }
}