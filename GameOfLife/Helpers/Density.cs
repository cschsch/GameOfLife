using System;

namespace GameOfLife.Helpers
{
    public struct Density
    {
        public double Value
        {
            get => _value;
            set
            {
                if (value < 0 || value > 1) throw new InvalidOperationException($"{nameof(value)} must be between 0 and 1");
                _value = value;
            }
        }

        private double _value;

        public Density(double value)
        {
            _value = 0;
            Value = value;
        }

        public static Density MaxValue => new Density(1);
        public static Density MinValue => new Density(0);
        public static implicit operator Density(double other) => new Density(other);
        public static implicit operator double(Density other) => other.Value;
    }
}