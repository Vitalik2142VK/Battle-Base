using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement.Air
{
    [CreateAssetMenu(
        fileName = nameof(AirMoveComponentSource),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + AssetMenuPaths.Movement
        + nameof(AirMoveComponentSource))]
    public class AirMoveComponentSource : ActorComponentSource, IAirMoveComponentSource
    {
        [SerializeField] private MoveConfig _moveConfig;
        [SerializeField][Min(1f)] private float _height = 20f;

        public IMoveConfig Config => _moveConfig;

        public float Height => _height;
    }
}
