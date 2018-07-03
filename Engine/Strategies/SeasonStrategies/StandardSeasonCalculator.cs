using System.Collections.Generic;

using Engine.Entities.Environmental;
using Engine.Helpers;

namespace Engine.Strategies.SeasonStrategies
{
    public class StandardSeasonCalculator : ICalculateSeason
    {
        private readonly IReadOnlyDictionary<Seasons, Season> _seasonChanger = new Dictionary<Seasons, Season>
        {
            {Seasons.Spring, Season.Summer},
            {Seasons.Summer, Season.Autumn},
            {Seasons.Autumn, Season.Winter},
            {Seasons.Winter, Season.Spring}
        };

        public Season GetSeasonOfNextTick(Season currentSeason) =>
            currentSeason.CurrentTime >= currentSeason.Length
                ? _seasonChanger[currentSeason.Id]
                : new Season(currentSeason) {CurrentTime = currentSeason.CurrentTime + 1};

        // these calculations have been chosen more or less deliberately to kind of simulate a shift in seasons regarding temperature
        // they are not meant to represent realistic values
        public double CalculateTemperature(Season season) => new Match<Season, double>(
                (s => s.Id == Seasons.Spring, s => (s.CurrentTime * 3) - s.Length), // linear ascending
                (s => s.Id == Seasons.Summer, s => (-0.6 * s.CurrentTime * s.CurrentTime) + (s.Length / 2 * s.CurrentTime) + (s.Length * 2)), // parabola facing down
                (s => s.Id == Seasons.Autumn, s => (-s.CurrentTime * 3) + s.Length), // linear descending
                (s => s.Id == Seasons.Winter, s => (0.6 * s.CurrentTime * s.CurrentTime) - (s.Length / 2 * s.CurrentTime) - (s.Length * 2))) // parabola facing up
            .MatchFirst(season);
    }
}