using System;
using UnityEngine;
using UnityEngine.AI;
using BattleBase.Utils.Extensions;

namespace BattleBase.Gameplay.Actors.Movement
{
    [PreviewExcluded(PreviewExclusionMode.Remove)]
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavigationAgent : MonoBehaviour, INavigationAgent
    {
        [SerializeField][Min(0.1f)] private float _distanceFinish = 0.5f;

        private IMoverPresenter _presenter;
        private IMoveConfig _config;
        private IMoverEvents _moverEvents;
        private IWaypoint _waypoint;
        private Transform _transform;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _transform = transform;
            _agent = GetComponent<NavMeshAgent>();

            if (_agent.isOnNavMesh)
                _agent.isStopped = true;
        }

        private void OnEnable()
        {
            if (_moverEvents != null)
            {
                _moverEvents.WaypointChanged += OnSetWaypoint;
                _moverEvents.Moved += OnMove;
                _moverEvents.Stoped += OnStop;
            }
        }

        private void FixedUpdate()
        {
            if (_waypoint == null)
                return;

            if (_transform.position.IsWithinDistance(_waypoint.Position, _distanceFinish + _agent.stoppingDistance)
                && _agent.isStopped == false)
            {
                _presenter.ReachPoint();
            }
        }

        private void OnDisable()
        {
            if (_moverEvents != null)
            {
                _moverEvents.WaypointChanged -= OnSetWaypoint;
                _moverEvents.Moved -= OnMove;
                _moverEvents.Stoped -= OnStop;
            }
        }

        public void Init(IMoverPresenter presenter, IMoveConfig config, IMoverEvents moverEvents)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _moverEvents = moverEvents ?? throw new ArgumentNullException(nameof(moverEvents));

            if (gameObject.activeSelf)
            {
                _moverEvents.WaypointChanged += OnSetWaypoint;
                _moverEvents.Moved += OnMove;
                _moverEvents.Stoped += OnStop;
            }
        }

        private void OnSetWaypoint(IWaypoint waypoint)
        {
            _waypoint = waypoint ?? throw new ArgumentNullException(nameof(waypoint));
        }

        private void OnMove()
        {
            if (_agent.isStopped == true)
                _agent.isStopped = false;

            _agent.speed = _config.Speed;
            _agent.angularSpeed = _config.AngularSpeed;
            _agent.acceleration = _config.Acceleration;
            _agent.stoppingDistance = _config.StoppingDistance;
            _agent.SetDestination(_waypoint.Position);
        }

        private void OnStop() => 
            _agent.isStopped = true;
    }
}
