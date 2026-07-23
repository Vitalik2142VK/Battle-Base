using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public class VelocityRotation : MonoBehaviour
    {
        [SerializeField][Range(0.00001f, 0.001f)] private float _minSqrDistance = 0.0001f;

        private Transform _transform;
        private Vector3 _previousPosition;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _previousPosition = _transform.position;
        }

        private void LateUpdate()
        {
            Vector3 direction = _transform.position - _previousPosition;

            if (direction.sqrMagnitude > _minSqrDistance)
                _transform.rotation = Quaternion.LookRotation(direction);

            _previousPosition = _transform.position;
        }
    }
}