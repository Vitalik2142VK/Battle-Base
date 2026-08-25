using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement.Hunt
{
    [CreateAssetMenu(
        fileName = nameof(HuntMoveComponentSource),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + AssetMenuPaths.Movement
        + nameof(HuntMoveComponentSource))]
    public class HuntMoveComponentSource : ActorComponentSource, IHuntMoveComponentSource
    {
        [SerializeField] private MoveComponentSource _moveComponentSource;
        [SerializeField] private Vector3 _offset;
        [SerializeField][Min(0)] private float _stoppingDistanceAttack;

        public IMoveComponentSource MoveComponent => _moveComponentSource;

        public Vector3 Offset => _offset;

        public float StoppingDistanceAttack => _stoppingDistanceAttack;
    }
}
