using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    [CreateAssetMenu(
    fileName = nameof(ImprovementSource),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + "/" + nameof(ImprovementSource))]
    public class ImprovementSource : ActorComponentSource, IImprovementSource
    {
        [SerializeField] private ImprovementData _improvementData;

        public IImprovementData Data => _improvementData;
    }
}