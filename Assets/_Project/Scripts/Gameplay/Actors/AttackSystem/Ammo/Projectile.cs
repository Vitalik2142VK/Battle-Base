using BattleBase.Core;
using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public abstract class Projectile : MonoBehaviour, IProjectile, IPoolable<Projectile>
    {
        public abstract event Action<Projectile> Deactivated;

        public string Id => name;

        protected IDamage Damage { get; private set; }

        protected IProjectileConfig Config { get; private set; }

        public virtual void SetProjectileConfig(IProjectileConfig config) => 
            Config = config ?? throw new ArgumentNullException(nameof(config));

        public virtual void SetDamage(IDamage damage) => 
            Damage = damage ?? throw new ArgumentNullException(nameof(damage));

        public abstract void ShootTarget(IShotPointTransform shotPointTransform, ITarget target);
    }
}