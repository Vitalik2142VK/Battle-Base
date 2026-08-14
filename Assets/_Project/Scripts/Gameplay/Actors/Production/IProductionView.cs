using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production
{
    public interface IProductionView : IActorViewComponent
    {
        public TeamType TeamType { get; }

        public int BuildingSiteId { get; }

        public void Init(IProductionPresenter presenter, ITeamable teamable);
    }
}