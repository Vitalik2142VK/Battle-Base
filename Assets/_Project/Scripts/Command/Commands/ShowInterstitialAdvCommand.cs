using System;
using BattleBase.AdvService;
using BattleBase.DI;
using VContainer;

namespace BattleBase.Commands
{
    public class ShowInterstitialAdvCommand : CommandBase, IInjectable
    {
        private IAdvService _advService;

        [Inject]
        public void Construct(IAdvService advService) =>
            _advService = advService ?? throw new ArgumentNullException(nameof(advService));

        public override void Execute() =>
            _advService.ShowInterstitialAdv();
    }
}