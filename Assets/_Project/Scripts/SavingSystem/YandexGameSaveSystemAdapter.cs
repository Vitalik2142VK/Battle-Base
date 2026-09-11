using System;
using BattleBase.Utils.Constants;
using YG;

namespace BattleBase.SaveService
{
    public class YandexGameSaveSystemAdapter : ISaver
    {
        private bool _isDirty;

        public event Action ProgressReseted;

        public IVolumeData VolumeData => JsonData.VolumeData;

        public IColorData ColorData => JsonData.ColorData;

        public ITerritoryData TerritoryData => JsonData.TerritoryData;

        public IShopData ShopData => JsonData.ShopData;

        private JsonSavesData JsonData => YG2.saves.SavesData;

        public bool IsNoAds => YG2.GetState(PurchasesKeys.NoAdsKey) == 1;

        public void SaveProgress()
        {
            if (_isDirty)
            {
                YG2.SaveProgress();
                _isDirty = false;
            }
        }

        public void ResetProgress()
        {
            YG2.SetDefaultSaves();
            _isDirty = true;
            SaveProgress();

            ProgressReseted?.Invoke();
        }

        public void SetVolumeData(IVolumeData data)
        {
            if (JsonData.VolumeData.IsChangedFrom(data))
            {
                JsonData.SetVolumeData(data);
                _isDirty = true;
            }
        }

        public void SetColorData(IColorData data)
        {
            if (JsonData.ColorData.IsChangedFrom(data))
            {
                JsonData.SetColorData(data);
                _isDirty = true;
            }
        }

        public void SetTerritoryData(ITerritoryData data)
        {
            if (JsonData.TerritoryData.IsChangedFrom(data))
            {
                JsonData.SetTerritoryData(data);
                _isDirty = true;
            }
        }

        public void SetShopData(IShopData data)
        {
            if (JsonData.ShopData.IsChangedFrom(data))
            {
                JsonData.SetShopData(data);
                _isDirty = true;
            }
        }

        public void SetNoAdsState(bool isOn) =>
            YG2.SetState(PurchasesKeys.NoAdsKey, isOn ? 1 : 0);
    }
}