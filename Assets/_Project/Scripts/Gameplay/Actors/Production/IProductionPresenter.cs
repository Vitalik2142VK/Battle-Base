using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production
{
    public interface IProductionPresenter
    {
        public IEnumerable<IProductionOption> ProductionOptions { get; }
    }
}