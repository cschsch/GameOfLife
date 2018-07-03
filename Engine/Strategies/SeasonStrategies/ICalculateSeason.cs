using Engine.Entities.Environmental;

namespace Engine.Strategies.SeasonStrategies
{
    /// <summary>
    /// Needed to calculate the temperature of the next tick using Seasons
    /// </summary>
    public interface ICalculateSeason
    {
        /// <summary>
        /// Gets the next tick's season according to <paramref name="currentSeason"/>
        /// </summary>
        /// <param name="currentSeason">The current season to start from</param>
        /// <returns>The next tick's season</returns>
        Season GetSeasonOfNextTick(Season currentSeason);

        /// <summary>
        /// Calculates the temperature based on <paramref name="season"/>
        /// </summary>
        /// <param name="season">Current season</param>
        /// <returns>Calculated temperature</returns>
        double CalculateTemperature(Season season);
    }
}