using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

namespace BattleBase.Gameplay.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Mover : MonoBehaviour, IMover
    {
        [SerializeField] private GameObject _target;

        private Transform _transform;
        private NavMeshAgent _agent;
        private Vector3 _targetPoint;
        private float distanceFinish;

        private void FixedUpdate()
        {
            if (Vector3.Distance(_targetPoint, _transform.position) < distanceFinish
                && _agent.isStopped == false)
                Stop();
        }

        public void Init(IMovementConfig config)
        {
            if (config == null)
                throw new System.ArgumentNullException(nameof(config));

            _transform = transform;

            if (_target != null)
                _targetPoint = _target.transform.position;
            else
                _targetPoint = Vector3.zero;

            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = config.Speed;
            _agent.angularSpeed = config.AngularSpeed;
            _agent.acceleration = config.Acceleration;
            _agent.stoppingDistance = config.StoppingDistance;
        }

        public void Move()
        {
            if (_agent.isStopped == true)
                _agent.isStopped = false;

            _agent.SetDestination(_targetPoint);
        }

        public void Stop() => _agent.isStopped = true;
    }
}
