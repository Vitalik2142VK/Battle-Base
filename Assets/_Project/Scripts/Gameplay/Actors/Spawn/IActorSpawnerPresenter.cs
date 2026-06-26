using BattleBase.Gameplay.Actors.Production;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawnerPresenter
    {
        public IEnumerable<ProductionOption> ProductionOptions { get; }
    }
}