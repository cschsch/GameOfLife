namespace Engine.Helpers.Functions
{
    public static class DoubleExtensions
    {
        /// <summary>
        /// Divides <paramref name="divisor"/> by <paramref name="dividend"/> and returns 0 instead of Infinity in case of <paramref name="dividend"/> being 0
        /// </summary>
        /// <param name="divisor">Divisord</param>
        /// <param name="dividend">Dividend</param>
        /// <returns>Value of division or 0</returns>
        public static double DivideSkipZeroDivisor(this double divisor, double dividend) =>
            dividend.Equals(0) ? 0 : divisor / dividend;
    }
}