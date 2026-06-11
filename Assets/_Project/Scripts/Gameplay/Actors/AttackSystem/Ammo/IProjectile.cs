using BattleBase.Gameplay.Actors.DamageSystem;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public interface IProjectile
    {
        public void SetProjectileConfig(IProjectileConfig config);

        public void SetDamage(IDamage damage);

        public void ShootTarget(Vector3 startPosition, ITarget target);
    }
}