using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production
{
    public interface IProductionView : IActorViewComponent
    {
        public IEnumerable<IProductionOption> ProductionOptions { get; }

        public TeamType TeamType { get; }

        public void Init(IProductionPresenter presenter, ITeamable teamable);
    }
}