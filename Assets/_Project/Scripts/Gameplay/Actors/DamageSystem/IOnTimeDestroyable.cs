namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public interface IOnTimeDestroyable : IActorComponent, IDestroyableEvents
    {
        public void Destroy();
    }
}