using System;
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

        public int GetState(string key) =>
            YG2.GetState(key);

        public void SetState(string key, int value) =>
            YG2.SetState(key, value);
    }
}