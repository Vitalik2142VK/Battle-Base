using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement.Jet
{
    [CreateAssetMenu(
        fileName = nameof(JetMoveComponentSource),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + AssetMenuPaths.Movement
        + nameof(JetMoveComponentSource))]
    public class JetMoveComponentSource : ActorComponentSource, IJetMoveComponentSource
    {
        [SerializeField] private MoveConfig _moveConfig;
        [SerializeField][Min(1f)] private float _height = 20f;
        [SerializeField][Min(10f)] private float _offsetUturn = 20f;

        public IMoveConfig Config => _moveConfig;

        public float Height => _height;

        public float OffsetUturn => _offsetUturn;
    }
}
