using System.Collections.Generic;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [CreateAssetMenu(
        fileName = nameof(ActorsUpgradeConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorsUpgradeConfig))]
    public class ActorsUpgradeConfig : ScriptableObject
    {
        [SerializeField] private List<ShopActorItemConfig> _infos;

        public IReadOnlyList<IShopActorItemConfig> Infos => _infos;
    }
}