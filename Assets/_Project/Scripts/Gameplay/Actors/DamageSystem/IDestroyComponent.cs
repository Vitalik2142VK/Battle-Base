namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public interface IDestroyComponent : IActorComponent, IDestroyableEvent
    {
        public void AddDestroyableEvent(IDestroyableEvent damagebleEvent);
    }
}