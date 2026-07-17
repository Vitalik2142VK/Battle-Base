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
        [SerializeField] private UnitNameConfig _unitNameConfig;
        [SerializeField] private UnitDescriptionConfig _unitDescriptionConfig;

        [Header("ScreenshotParams")]
        [SerializeField] private GameObject _cleanPrefab;
        [SerializeField] private float _previewScreenScale = 1;        

        public string Id => _actorConfig.Data.Id;

        public ILanguageTextsSet UnitName => _unitNameConfig;

        public ILanguageTextsSet Description => _unitDescriptionConfig;

        public IShopUpgradeStatsInfo PanelInfo => _stats;

        public GameObject CleanPrefab => _cleanPrefab;

        public float PreviewScreenScale => _previewScreenScale;
    }
}