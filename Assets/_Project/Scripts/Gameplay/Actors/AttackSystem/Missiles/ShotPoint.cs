using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Missiles
{
    public class ShotPoint : MonoBehaviour, IShotPoint
    {
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public Vector3 Position => _transform.position;
    }
}