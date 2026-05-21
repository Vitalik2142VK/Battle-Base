using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorViewSpawner : IActorViewComponent 
    {
        public IBuildingSite BuildingSite { get; }

        public IEnumerable<IActorData> ActorsData { get; }

        public void Init(IActorSpawnerPresenter presenter, IActorSpawnerEvents events);

        public void SelectActorData(IActorData actorData);

        public void SetBuildingSite(IBuildingSite buildingSite);
    }
}
