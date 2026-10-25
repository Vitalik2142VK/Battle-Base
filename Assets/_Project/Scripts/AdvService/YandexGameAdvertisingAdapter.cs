using System;
using YG;

namespace BattleBase.AdvService
{
    public class YandexGameAdvertisingAdapter : IAdvertisingService, IDisposable
    {
        public YandexGameAdvertisingAdapter()
        {
            YG2.onRewardAdv += OnReward;
            YG2.onErrorRewardedAdv += OnCloseReward;
            YG2.onCloseRewardedAdv += OnCloseReward;
        }

        public event Action<string> RewardAdvShown;
        public event Action RewardAdvClosed;

        public void Dispose()
        {
            YG2.onRewardAdv -= OnReward;
            YG2.onErrorRewardedAdv -= OnCloseReward;
            YG2.onCloseRewardedAdv -= OnCloseReward;
        }

        public void ShowInterstitialAdv() =>
            YG2.InterstitialAdvShow();

        public void ShowRewardedAdv(string id) =>
            YG2.RewardedAdvShow(id);

        public void ShowRewardedAdv(string id, Action callback) =>
            YG2.RewardedAdvShow(id, callback);

        public void SetActivityStickyAd(bool isActive) =>
            YG2.StickyAdActivity(isActive);

        private void OnReward(string id) =>
            RewardAdvShown?.Invoke(id);

        private void OnCloseReward() =>
            RewardAdvClosed?.Invoke();
    }
}