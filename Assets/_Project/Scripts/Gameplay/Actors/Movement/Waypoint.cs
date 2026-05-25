using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement
{
    public class Waypoint : MonoBehaviour, IWaypoint
    {
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public Vector3 Position => _transform.position;
    }
}