namespace Engine.Entities.Standard
{
    public class StandardCell : BaseCell
    {
        public StandardCell() { }

        public StandardCell(BaseCell other)
        {
            IsAlive = other.IsAlive;
            LifeTime = other.LifeTime;
        }
    }
}