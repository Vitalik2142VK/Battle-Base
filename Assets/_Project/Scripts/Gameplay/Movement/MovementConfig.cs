using BattleBase.Gameplay.Units;
using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Movement
{
    [CreateAssetMenu(
        fileName = nameof(MovementConfig),
        menuName = Constants.ConfigsAssetMenuName + "/" + nameof(UnitConfig) + "/" + nameof(MovementConfig))]
    public class MovementConfig : ScriptableObject, IMovementConfig
    {
        [SerializeField][Min(0.5f)] private float _speed = 3.5f;
        [SerializeField][Min(45f)] private float _angularSpeed = 120f;
        [SerializeField][Min(1f)] private float _acceleration = 10f;
        [SerializeField][Min(0.5f)] private float _stoppingDistance = 3f;
        [SerializeField][Min(0.1f)] private float _distanceFinish = 0.1f;

        public float Speed => _speed;

        public float AngularSpeed => _angularSpeed;

        public float Acceleration => _acceleration;

        public float StoppingDistance => _stoppingDistance;

        public float DistanceFinish => _distanceFinish;
    }
}
