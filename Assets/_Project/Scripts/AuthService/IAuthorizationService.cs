using System;

namespace BattleBase.AuthService
{
    public interface IAuthorizationService
    {
        public event Action SDKDataReceived;

        public bool IsAuth {  get; }
    }
}