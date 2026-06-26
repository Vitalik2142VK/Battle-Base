using BattleBase.Gameplay.Actors.Production;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawner : IActorComponent, IUpdateable, IActorSpawnerEvents, IProductionStorage
    {
        public void Init(ITeamable teamable, ISpawnPoint spawnData);

        public void SelectActorData(IActorData actorData);
    }
}