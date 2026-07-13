using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public interface IShotPointTransform
    {
        public Vector3 Position { get; }

        public Quaternion Rotation { get; }
    }
}