namespace BattleBase.Gameplay.Actors 
{
    public interface IActorView
    {
        public bool TryGetViewComponent<T>(out T component) where T : class, IActorViewComponent;
    }
}
