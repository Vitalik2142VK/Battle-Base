using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement.Air
{
    [CreateAssetMenu(
        fileName = nameof(AirMoveComponentSource),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + AssetMenuPaths.Movement
        + nameof(AirMoveComponentSource))]
    public class AirMoveComponentSource : MoveComponentSource, IAirMoveComponentSource
    {
        [SerializeField][Min(1f)] private float _height = 20f;

        public float Height => _height;
    }
}
