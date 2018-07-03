namespace Engine.Helpers.Functions
{
    public static class DoubleExtensions
    {
        public static double DivideSkipZeroDivisor(this double divisor, double dividend) =>
            dividend.Equals(0) ? 0 : divisor / dividend;
    }
}