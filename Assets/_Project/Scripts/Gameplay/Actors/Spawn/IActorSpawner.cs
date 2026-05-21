using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawner : IActorComponent, IUpdateable, IActorSpawnerEvents
    {
        public IEnumerable<IActorData> ActorsData { get; }

        public void SelectActorData(IActorData actorData);

        public void Init(ITeamable teamable);
    }
}