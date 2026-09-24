using System;
using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [Serializable]
    public class ActorItemInfo : IActorItemConfig
    {
        [SerializeField] private string _id;
        [SerializeField] private LanguageTextsSet _unitName;
        [SerializeField] private LanguageTextsSet _unitDescription;
        [SerializeField] private ShopUpgradeStatsInfo _panelInfo;
        [SerializeField] private GameObject _sourcePrefab;
        [SerializeField] private float _previewScreenScale;
        [SerializeField] private bool _isAvailable;

        public ActorItemInfo(IActorItemConfig other)
        {
            _id = other.Id;
            _unitName = new(other.UnitName);
            _unitDescription = new(other.Description);
            _panelInfo = new(other.PanelInfo);
            _sourcePrefab = other.SourcePrefab;
            _previewScreenScale = other.PreviewScreenScale;
            _isAvailable = other.IsAvailable;
        }

        public string Id => _id;

        public ILanguageTextsSet UnitName => _unitName;

        public ILanguageTextsSet Description => _unitDescription;

        public IShopUpgradeStatsInfo PanelInfo => _panelInfo;

        public GameObject SourcePrefab => _sourcePrefab;

        public float PreviewScreenScale => _previewScreenScale;

        public bool IsAvailable => _isAvailable;

        public void IncreaseDamageLevel() =>
            _panelInfo.IncreaseDamageLevel();

        public void IncreaseArmorLevel() =>
            _panelInfo.IncreaseArmorLevel();

        public void IncreaseBuildTimeLevel() =>
            _panelInfo.IncreaseBuildTimeLevel();

        public void SetDamageLevel(int level) =>
            _panelInfo.SetDamageLevel(level);

        public void SetArmorLevel(int level) =>
            _panelInfo.SetArmorLevel(level);

        public void SetBuildTimeLevel(int level) =>
            _panelInfo.SetBuildTimeLevel(level);
    }
}