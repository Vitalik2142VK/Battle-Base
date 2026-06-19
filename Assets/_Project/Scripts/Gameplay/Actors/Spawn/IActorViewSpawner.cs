using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorViewSpawner : IActorViewComponent, ISpawnData
    {
        public IEnumerable<IActorData> ActorsData { get; }

        public void Init(IActorSpawnerPresenter presenter);

        public void SelectActorData(IActorData actorData);
    }
}
