using System;

namespace BattleBase.SaveService
{
    public interface IPurchasesSaver
    {
        public event Action<bool> NoAdsStateChanged;

        public bool IsNoAds { get; }

        public void SetNoAdsState(bool isOn);
    }
}