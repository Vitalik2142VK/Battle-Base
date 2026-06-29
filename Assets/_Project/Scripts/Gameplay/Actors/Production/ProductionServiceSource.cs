using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Production
{
    [CreateAssetMenu(
        fileName = nameof(ProductionServiceSource),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + "/" + nameof(ProductionServiceSource))]
    public class ProductionServiceSource : ActorComponentSource, IComponentSource { }
}