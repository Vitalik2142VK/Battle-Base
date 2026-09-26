namespace BattleBase.SaveService
{
    public interface IChangeTrackable<T>
    {
        public bool IsDiffersFrom(T other);
    }
}