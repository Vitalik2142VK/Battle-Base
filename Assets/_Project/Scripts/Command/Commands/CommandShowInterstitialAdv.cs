using System;
using BattleBase.AdvService;
using BattleBase.DI;
using BattleBase.SaveService;
using VContainer;

namespace BattleBase.Commands
{
    public sealed class CommandShowInterstitialAdv : CommandBase, IInjectable
    {
        private IAdvertisingService _advService;
        private IPurchasesSaver _saver;

        [Inject]
        public void Construct(IAdvertisingService advService, IPurchasesSaver saver)
        {
            _advService = advService ?? throw new ArgumentNullException(nameof(advService));
            _saver = saver ?? throw new ArgumentNullException(nameof(saver));
        }

        public override void Execute()
        {
            if (_saver.IsNoAds == false)
                _advService.ShowInterstitialAdv();
        }
    }
}