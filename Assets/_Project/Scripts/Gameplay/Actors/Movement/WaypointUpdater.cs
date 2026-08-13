using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement
{
    public class WaypointUpdater : IWaypoint
    {
        private readonly IWaypoint _waypoint;
        private readonly Vector3 _offset;

        public WaypointUpdater(IWaypoint waypoint, Vector3 offset)
        {
            _waypoint = waypoint ?? throw new System.ArgumentNullException(nameof(waypoint));
            _offset = offset;
        }

        public Vector3 Position => _waypoint.Position + _offset;
    }
}
