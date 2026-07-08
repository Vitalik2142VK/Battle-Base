using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    [CreateAssetMenu(
    fileName = nameof(ImproverSource),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + "/" + nameof(ImproverSource))]
    public class ImproverSource : ActorComponentSource, IImproverSource
    {
        [SerializeField] private ImproverData _improvementData;

        public IImproverData Data => _improvementData;
    }
}