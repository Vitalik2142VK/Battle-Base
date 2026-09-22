using BattleBase.Gameplay.Actors;
using BattleBase.Localization;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [CreateAssetMenu(
        fileName = nameof(ActorItemConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorItemConfig))]
    public class ActorItemConfig : ScriptableObject, IActorItemConfig
    {
        [SerializeField] private ActorConfig _actorConfig;
        [SerializeField] private UpgradeStatsConfig _stats;
        [SerializeField] private ActorNameConfig _unitNameConfig;

        [Header("ScreenshotParams")]
        [SerializeField] private GameObject _sourcePrefab;
        [SerializeField] private float _previewScreenScale = 1;

        public string Id => _actorConfig.Data.Id;

        public ILanguageTextsSet UnitName => _unitNameConfig.Name;

        public ILanguageTextsSet Description => _unitNameConfig.Description;

        public IShopUpgradeStatsInfo PanelInfo => _stats;

        public GameObject SourcePrefab => _sourcePrefab;

        public float PreviewScreenScale => _previewScreenScale;

        public bool IsAvailable => _actorConfig.Data.IsAvailable;
    }
}