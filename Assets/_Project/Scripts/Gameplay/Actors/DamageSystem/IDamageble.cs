namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public interface IDamageble : IDestroyableEvent
    {
        public void TakeDamage(IDamage damage);
    }
}