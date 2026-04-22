namespace BattleBase.Core
{
    public interface IFactory<T>
    {
        T Create();
    }
}