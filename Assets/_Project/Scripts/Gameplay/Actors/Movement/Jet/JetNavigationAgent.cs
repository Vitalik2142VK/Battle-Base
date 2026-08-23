using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement.Jet
{
    public class JetNavigationAgent : MonoBehaviour, INavigationAgent
    {
        [SerializeField][Min(0.1f)] private float _distanceFinish = 0.5f;

        private IMoverPresenter _presenter;
        private IMoverEvents _moverEvents;
        private IWaypoint _waypoint;
        private IMoveConfig _config;
        private Transform _transform;

        private bool _isMoving;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            if (_moverEvents == null)
                return;

            _moverEvents.WaypointChanged += OnSetWaypoint;
            _moverEvents.Moved += OnMove;
            _moverEvents.Stoped += OnStop;
        }

        private void OnDisable()
        {
            if (_moverEvents == null)
                return;

            _moverEvents.WaypointChanged -= OnSetWaypoint;
            _moverEvents.Moved -= OnMove;
            _moverEvents.Stoped -= OnStop;
        }

        private void Update()
        {
            if (_isMoving == false || _waypoint == null)
                return;

            Move(Time.deltaTime);
        }

        public void Init(IMoverPresenter presenter, IMoveConfig config, IMoverEvents moverEvents)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _moverEvents = moverEvents ?? throw new ArgumentNullException(nameof(moverEvents));
            _config = config ?? throw new ArgumentNullException(nameof(config));

            if (gameObject.activeSelf)
            {
                _moverEvents.WaypointChanged += OnSetWaypoint;
                _moverEvents.Moved += OnMove;
                _moverEvents.Stoped += OnStop;
            }
        }

        private void Move(float deltaTime)
        {
            Vector3 direction = _waypoint.Position - _transform.position;

            if (direction.sqrMagnitude <= _distanceFinish * _distanceFinish * _config.StoppingDistance)
            {
                ReachPoint();

                return;
            }

            direction.Normalize();

            RotateTowards(direction, deltaTime);

            _transform.position += _transform.forward * (_config.Speed * deltaTime);
        }

        private void RotateTowards(Vector3 direction, float deltaTime)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            _transform.rotation = Quaternion.RotateTowards(
                _transform.rotation,
                targetRotation,
                _config.AngularSpeed * deltaTime);
        }

        private void ReachPoint()
        {
            _isMoving = false;
            _presenter.ReachPoint();
        }

        private void OnSetWaypoint(IWaypoint waypoint)
        {
            _waypoint = waypoint ?? throw new ArgumentNullException(nameof(waypoint));
        }

        private void OnMove()
        {
            if (_waypoint == null)
                return;

            _isMoving = true;
        }

        private void OnStop()
        {
            _isMoving = false;
        }
    }
}
