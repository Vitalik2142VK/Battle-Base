using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawner : IActorComponent, IUpdateable, IActorSpawnerEvents
    {
        public IEnumerable<IActorData> ActorsData { get; }

        public void Init(ITeamable teamable, ISpawnData spawnData);

        public void SelectActorData(IActorData actorData);
    }
}