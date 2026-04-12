using UnityEngine;
using UnityEngine.AI;

namespace BattleBase.Gameplay.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Mover : MonoBehaviour, IMover
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private MovementConfig _config;

        private Transform _transform;
        private NavMeshAgent _agent;
        private Vector3 _targetPoint;

        private void OnValidate()
        {
            if (_config == null)
                throw new System.NullReferenceException(nameof(_config));
        }

        private void Awake()
        {
            _transform = transform;

            if (_target != null)
                _targetPoint = _target.transform.position;
            else
                _targetPoint = Vector3.zero;

            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _config.Speed;
            _agent.angularSpeed = _config.AngularSpeed;
            _agent.acceleration = _config.Acceleration;
            _agent.stoppingDistance = _config.StoppingDistance;
        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(_targetPoint, _transform.position) < _config.DistanceFinish 
                && _agent.isStopped == false)
                Stop();
        }

        public void Move()
        {
            if (_agent.isStopped == true)
                _agent.isStopped = false;

            _agent.SetDestination(_targetPoint);
        }

        private void Stop() => _agent.isStopped = true;
    }
}
