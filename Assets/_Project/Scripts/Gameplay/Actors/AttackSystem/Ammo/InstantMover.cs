using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public class InstantMover : MonoBehaviour
    {
        private Transform _transform;
        private Vector3 _pointPosition;

        public Vector3 CurrentPosition => _transform.position;

        private void Awake()
        {
            _transform = transform;
        }

        public void SetStartPosition(Vector3 startPosition) => 
            _transform.position = startPosition;

        public void SetPointPosition(Vector3 point) => 
            _pointPosition = point;

        public void Move() => 
            _transform.position = _pointPosition;
    }
}