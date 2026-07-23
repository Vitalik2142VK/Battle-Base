namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public interface IOnTimeDestroyable : IActorComponent, IDestroyableEvent
    {
        public void Destroy();
    }
}