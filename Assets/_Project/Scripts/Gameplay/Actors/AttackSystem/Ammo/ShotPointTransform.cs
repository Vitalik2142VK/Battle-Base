using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public class ShotPointTransform : IShotPointTransform
    {
        private readonly Transform _transform;

        public ShotPointTransform(Transform transform)
        {
            if (transform == null)
                throw new System.ArgumentNullException(nameof(transform));

            _transform = transform;
        }

        public Vector3 Position => _transform.position;

        public Quaternion Rotation => _transform.rotation;
    }
}