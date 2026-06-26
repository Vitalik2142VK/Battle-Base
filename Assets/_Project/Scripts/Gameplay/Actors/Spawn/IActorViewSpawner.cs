using BattleBase.Gameplay.Actors.Production;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorViewSpawner : IActorViewComponent, ISpawnData
    {
        public IEnumerable<ProductionOption> ProductionOptions { get; }

        public TeamType TeamType { get; }

        public void Init(IActorSpawnerPresenter presenter, ITeamable teamable);
    }
}
