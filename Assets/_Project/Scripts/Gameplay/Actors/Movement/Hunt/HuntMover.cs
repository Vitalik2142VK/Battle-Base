using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Utils.Extensions;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Movement.Hunt
{
    public class HuntMover : IHuntMover
    {
        private readonly IMover _mover;
        private readonly IHuntMoveData _data;

        private IWaypoint _currentWaypoint;
        private IActorPosition _actorPosition;
        private TargetWaypoint _targetWaypoint;

        public event Action<IWaypoint> WaypointChanged;
        public event Action Moved;
        public event Action Stoped;

        public HuntMover(IMover mover, IHuntMoveData data)
        {
            _mover = mover ?? throw new ArgumentNullException(nameof(mover));
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public Type KeyType => typeof(IHuntMover);

        public IMoveConfig Config => _mover.Config;

        public bool CanMove => _mover.CanMove;

        public void Init(IActorPosition actorPosition)
        {
            _actorPosition = actorPosition ?? throw new ArgumentNullException(nameof(actorPosition));
        }

        public void Enable()
        {
            _mover.Moved += Moved;
            _mover.Stoped += Stoped;

            _mover.WaypointChanged += OnSetCurrentWaipoint;
            _mover.Enable();
        }

        public void Disable()
        {
            _mover.Moved -= Moved;
            _mover.Stoped -= Stoped;

            _mover.WaypointChanged -= OnSetCurrentWaipoint;
            _mover.Disable();
        }

        public void Update(float _)
        {
            if (_targetWaypoint == null || _data.StoppingDistanceAttack == 0)
                return;

            if (_actorPosition.Position.IsWithinDistance(_targetWaypoint.Position, _data.StoppingDistanceAttack))
                _mover.Stop();
            else
                _mover.Move();
        }

        public void EstablishTarget(ITarget target)
        {
            _targetWaypoint = new TargetWaypoint(target, _data.Offset);

            WaypointChanged?.Invoke(_targetWaypoint);
        }

        public void ResetTarget()
        {
            _targetWaypoint = null;

            WaypointChanged?.Invoke(_currentWaypoint);
        }

        public void EstablishWaypoints(IEnumerable<IWaypoint> waypoints) =>
            _mover.EstablishWaypoints(waypoints);

        public void EstablishNextPoint()
        {
            if (_targetWaypoint != null)
                WaypointChanged?.Invoke(_targetWaypoint);
            else
                _mover.EstablishNextPoint();
        }

        public void Move() =>
            _mover.Move();

        public void Stop() => 
            _mover.Stop();

        private void OnSetCurrentWaipoint(IWaypoint waypoint)
        {
            _currentWaypoint = waypoint ?? throw new ArgumentNullException(nameof(waypoint));

            WaypointChanged?.Invoke(_currentWaypoint);
        }
    }
}
