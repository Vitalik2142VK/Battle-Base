namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawner : IActorComponent, IActorDataStorage, IUpdateable, IActorSpawnerEvents
    {
        public void Init(ITeamable teamable, ISpawnPoint spawnData);

        public void SelectActorData(IActorData actorData);
    }
}