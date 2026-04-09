using System;
using BattleBase.Abstract;
using BattleBase.Services.PauseService;
using VContainer;

namespace BattleBase.Commands
{
    public class ResumeGameCommand : CommandBase, IInjectable
    {
        private IPauseSwitcher _pauseSwitcher;

        [Inject]
        public void Construct(IPauseSwitcher pauseSwitcher) =>
            _pauseSwitcher = pauseSwitcher ?? throw new ArgumentNullException(nameof(pauseSwitcher));

        public override void Execute() =>
            _pauseSwitcher.Resume();
    }
}