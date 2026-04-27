using BattleBase.Gameplay.DamageSystem;

namespace BattleBase.Gameplay.HealthSystem
{
    public interface IHealth : IDamageble
    {
        public bool IsAlive { get; }

        public void Restore();
    }
}