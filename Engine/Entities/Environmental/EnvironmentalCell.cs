using System;

namespace Engine.Entities.Environmental
{
    public class EnvironmentalCell : BaseCell, IEquatable<EnvironmentalCell>
    {
        public DietaryRestriction Diet { get; set; }

        public static char Carnivore => 'X';
        public static char Herbivore => 'O';

        public bool Equals(EnvironmentalCell other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Diet == other.Diet;
        }

        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != GetType()) return false;
            return ReferenceEquals(this, obj) || Equals((EnvironmentalCell) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (int) Diet;
            }
        }

        public static bool operator ==(EnvironmentalCell left, EnvironmentalCell right) => Equals(left, right);
        public static bool operator !=(EnvironmentalCell left, EnvironmentalCell right) => !Equals(left, right);
    }

    public enum DietaryRestriction
    {
        None, Carnivore, Herbivore, End
    }
}