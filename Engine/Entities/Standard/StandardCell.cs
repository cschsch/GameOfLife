using System;

namespace Engine.Entities.Standard
{
    public class StandardCell : BaseCell, IEquatable<StandardCell>
    {
        public bool Equals(StandardCell other) => base.Equals(other);
        public static bool operator ==(StandardCell left, StandardCell right) => Equals(left, right);
        public static bool operator !=(StandardCell left, StandardCell right) => !Equals(left, right);
    }
}