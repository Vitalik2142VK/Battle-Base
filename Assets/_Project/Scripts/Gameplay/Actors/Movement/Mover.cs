using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Movement
{
    public class Mover : IMover
    {
        private readonly Queue<IWaypoint> _waypoints;
        private readonly ModifiedMoveConfig _config;

        private IWaypoint _currentWaypoint;

        public event Action<IWaypoint> WaypointChanged;
        public event Action Moved;
        public event Action Stoped;

        public Mover(IMoveConfig moveConfig)
        {
            _waypoints = new Queue<IWaypoint>();
            _config = new ModifiedMoveConfig(moveConfig);
        }

        public Type KeyType => typeof(IMover);

        public IMoveConfig Config => _config;

        public bool CanMove => _currentWaypoint != null;

        public void Enable()
        {
            if (_waypoints.Count > 0)
                EstablishNextPoint();
        }

        public void Disable()
        {
            Stop();

            _config.Reset();
            _waypoints.Clear();
            _currentWaypoint = null;
        }

        public void EstablishWaypoints(IEnumerable<IWaypoint> waypoints)
        {
            if (waypoints == null)
                throw new ArgumentNullException(nameof(waypoints));

            foreach (IWaypoint waypoint in waypoints)
                _waypoints.Enqueue(waypoint);
        }

        public void EstablishNextPoint()
        {
            if (_waypoints.Count == 0)
            {
                _currentWaypoint = null;

                Stop();

                return;
            }

            _currentWaypoint = _waypoints.Dequeue();

            WaypointChanged?.Invoke(_currentWaypoint);
        }

        public void Move() => 
            Moved?.Invoke();

        public void Stop() => 
            Stoped?.Invoke();

        public void Upgrade(MoveConfigModificator modificator) =>
            _config.Modify(modificator);
    }
}
