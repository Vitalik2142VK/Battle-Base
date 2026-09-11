using BattleBase.Utils.Extensions;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement
{
    public class OffsetUnwrapper : MonoBehaviour
    {
        [SerializeField][Min(10f)] private float _offsetUturn = 20f;
        [SerializeField][Range(20f, 80f)] private float _angleForUturn = 45f;

        private IWaypoint _waypoint;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public bool TryPerformUturn(IWaypoint waypoint, out IWaypoint uturnWaipoint)
        {
            if (waypoint == null)
                throw new System.ArgumentNullException(nameof(waypoint));

            uturnWaipoint = null;
            _waypoint = null;

            Vector3 direction = waypoint.Position - _transform.position;

            if (_transform.position.IsWithinDistance(waypoint.Position, _offsetUturn) == false)
            {
                float angle = Vector3.Angle(_transform.forward, direction);

                if (angle <= _angleForUturn)
                    return false;
            }

            _waypoint = waypoint;
            Vector3 uturnPosition = _transform.position + _transform.forward * _offsetUturn;
            uturnWaipoint = new Waypoint(uturnPosition);

            return true;
        }

        public bool TryWithdraw(out IWaypoint waypoint)
        {
            waypoint = null;

            if (_waypoint == null)
                return false;

            waypoint = _waypoint;
            _waypoint = null;

            return true;
        } 
    }
}
