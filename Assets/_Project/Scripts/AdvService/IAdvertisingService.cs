using System;

namespace BattleBase.AdvService
{
    public interface IAdvertisingService
    {
        public event Action<string> RewardAdvShown;
        public event Action<string> PurchaseSuccess;

        public void ShowInterstitialAdv();

        public void ShowRewardedAdv(string id);

        public void ShowRewardedAdv(string id, Action callback);

        public void SetActivityStickyAd(bool isActive);
    }
}