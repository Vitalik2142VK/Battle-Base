using System;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public interface IProjectileMissEvent
    {
        public event Action Missed;
    }
}