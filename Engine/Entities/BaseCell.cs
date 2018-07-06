using System;

namespace Engine.Entities
{
    public abstract class BaseCell : IEquatable<BaseCell>
    {
        public bool IsAlive { get; set; }

        public int LifeTime
        {
            get => _lifeTime;
            set => _lifeTime = Math.Min(value, 10);
        }

        private int _lifeTime;

        public static char Alive => 'X';
        public static char DeadOut => ' ';
        public static char DeadIn => '-';

        public bool Equals(BaseCell other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return _lifeTime == other._lifeTime && IsAlive == other.IsAlive;
        }

        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != GetType()) return false;
            return ReferenceEquals(this, obj) || Equals((BaseCell)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (LifeTime * 397) ^ IsAlive.GetHashCode();
            }
        }

        public static bool operator ==(BaseCell left, BaseCell right) => Equals(left, right);
        public static bool operator !=(BaseCell left, BaseCell right) => !Equals(left, right);
    }
}