using BattleBase.Gameplay.Actors.DamageSystem;

namespace BattleBase.Gameplay.Actors.AttackSystem.Missiles
{
    public interface IMissileController
    {
        public void ShootMissile(ITarget target);
    }
}