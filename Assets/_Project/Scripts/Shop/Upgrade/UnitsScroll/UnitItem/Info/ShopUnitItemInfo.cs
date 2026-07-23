using System;
using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [Serializable]
    public class ShopUnitItemInfo : IShopActorItemConfig
    {
        [SerializeField] private string _id;
        [SerializeField] private LanguageTextsSet _unitName;
        [SerializeField] private LanguageTextsSet _unitDescription;
        [SerializeField] private ShopUpgradeStatsInfo _panelInfo;
        [SerializeField] private GameObject _cleanPrefab;
        [SerializeField] private float _previewScreenScale;

        public ShopUnitItemInfo(IShopActorItemConfig other)
        {
            _id = other.Id;
            _unitName = new(other.UnitName);
            _unitDescription = new(other.Description);
            _panelInfo = new(other.PanelInfo);
            _cleanPrefab = other.CleanPrefab;
            _previewScreenScale = other.PreviewScreenScale;
        }

        public string Id => _id;

        public ILanguageTextsSet UnitName => _unitName;

        public ILanguageTextsSet Description => _unitDescription;

        public IShopUpgradeStatsInfo PanelInfo => _panelInfo;

        public GameObject CleanPrefab => _cleanPrefab;

        public float PreviewScreenScale => _previewScreenScale;

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