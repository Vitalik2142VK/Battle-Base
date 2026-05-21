namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public interface IDamageble : IDestroyableEvents
    {
        public void TakeDamage(IDamage damage);
    }
}