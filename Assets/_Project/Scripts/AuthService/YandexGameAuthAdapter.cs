using System;
using YG;

namespace BattleBase.AuthService
{
    public class YandexGameAuthAdapter : IAuthorizationService, IDisposable
    {
        public YandexGameAuthAdapter()
        {
            YG2.onGetSDKData += OnGetSDKData;
        }

        public event Action SDKDataReceived;

        public bool IsAuth => YG2.player.auth;

        public void Dispose() =>
            YG2.onGetSDKData -= OnGetSDKData;

        private void OnGetSDKData() =>
            SDKDataReceived?.Invoke();
    }
}