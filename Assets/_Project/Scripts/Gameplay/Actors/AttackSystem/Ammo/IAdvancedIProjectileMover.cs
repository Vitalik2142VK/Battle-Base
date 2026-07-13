using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public interface IAdvancedIProjectileMover : IProjectileMover
    {
        public void SetStartRotation(Quaternion startRotation);
    }
}
