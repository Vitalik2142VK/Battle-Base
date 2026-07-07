using BattleBase.Gameplay.Actors;
using BattleBase.Localization;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [CreateAssetMenu(
        fileName = nameof(ShopActorItemConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(ShopActorItemConfig))]
    public class ShopActorItemConfig : ScriptableObject, IShopActorItemConfig
    {
        [SerializeField] private ActorConfig _actorConfig;
        [SerializeField] private ShopUpgradeStatsConfig _stats;

        public string Id => _actorConfig.Data.Id;

        public Sprite Preview => _actorConfig.Data.Icon;

        public ILanguageTextsSet UnitName => _actorConfig.Data.Name;

        public IShopUpgradeStatsInfo PanelInfo => _stats;
    }
}