using System.Collections.Generic;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [CreateAssetMenu(
        fileName = nameof(UnitsUpgradeConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(UnitsUpgradeConfig))]
    public class UnitsUpgradeConfig : ScriptableObject
    {
        [SerializeField] private List<ShopUnitItemInfo> _infos;

        public IReadOnlyList<IShopUnitItemInfo> Infos => _infos;
    }
}