using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement.Air
{
    public class AirMover : IMover
    {
        private readonly IMover _mover;
        private readonly float _height;

        public event Action<IWaypoint> WaypointChanged;
        public event Action Moved;
        public event Action Stoped;

        public AirMover(IMover mover, float height)
        {
            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height));

            _mover = mover ?? throw new ArgumentNullException(nameof(mover));
            _height = height;
        }

        public Type KeyType => _mover.KeyType;

        public IMoveConfig Config => _mover.Config;

        public bool CanMove => _mover.CanMove;

        public void Enable()
        {
            _mover.WaypointChanged += WaypointChanged;
            _mover.Moved += Moved;
            _mover.Stoped += Stoped;

            _mover.Enable();
        }

        public void Disable()
        {
            _mover.WaypointChanged -= WaypointChanged;
            _mover.Moved -= Moved;
            _mover.Stoped -= Stoped;

            _mover.Disable();
        }

        public void EstablishWaypoints(IEnumerable<IWaypoint> waypoints)
        {
            if (waypoints == null)
                throw new ArgumentNullException(nameof(waypoints));

            List<IWaypoint> changedWaypoints = new();

            foreach (var waypoint in waypoints)
            {
                Vector3 offset = new(0, waypoint.Position.y + _height, 0);
                WaypointUpdater updatedWaipoint = new(waypoint, offset);
                changedWaypoints.Add(updatedWaipoint);
            }

            _mover.EstablishWaypoints(changedWaypoints);
        }

        public void EstablishNextPoint() =>
            _mover.EstablishNextPoint();

        public void Move() => 
            _mover.Move();

        public void Stop() => 
            _mover.Stop();
    }
}
