using UnityEngine;
using UnityEngine.AI;

namespace BattleBase.Gameplay.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Mover : MonoBehaviour, IMover
    {
        private Transform _transform;
        private NavMeshAgent _agent;
        private Vector3 _pointPosition;
        private float _distanceFinish;

        private void FixedUpdate()
        {
            if (Vector3.Distance(_pointPosition, _transform.position) < _distanceFinish
                && _agent.isStopped == false)
                Stop();
        }

        public void Init(IMovementConfig config)
        {
            if (config == null)
                throw new System.ArgumentNullException(nameof(config));

            _transform = transform;

            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = config.Speed;
            _agent.angularSpeed = config.AngularSpeed;
            _agent.acceleration = config.Acceleration;
            _agent.stoppingDistance = config.StoppingDistance;
            _distanceFinish = config.DistanceFinish;
        }

        public void Stop() => _agent.isStopped = true;

        public void Move()
        {
            if (_agent.isStopped == true)
                _agent.isStopped = false;

            _agent.SetDestination(_pointPosition);
        }

        public void SetPointPosition(Vector3 pointPosition)
        {
            _pointPosition = pointPosition;
        }
    }
}
