using System;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public interface IProjectileHitEvent
    {
        public event Action Hited;
    }
}