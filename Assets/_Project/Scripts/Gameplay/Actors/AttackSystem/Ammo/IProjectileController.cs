using BattleBase.Gameplay.Actors.DamageSystem;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public interface IProjectileController
    {
        public void Shot(ITarget target, IDamage damage);
    }
}