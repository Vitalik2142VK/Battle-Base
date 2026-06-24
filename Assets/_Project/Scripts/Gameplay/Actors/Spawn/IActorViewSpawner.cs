using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorViewSpawner : IActorViewComponent, ISpawnData
    {
        public IEnumerable<IActorData> ActorsData { get; }

        public TeamType TeamType { get; }

        public void Init(IActorSpawnerPresenter presenter, ITeamable teamable);

        public void SelectActorData(IActorData actorData);
    }
}
