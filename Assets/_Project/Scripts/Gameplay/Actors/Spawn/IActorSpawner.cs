namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawner : IActorComponent, IActorDataStorage, IUpdateable, IActorSpawnerNotifier
    {
        public ISpawnerDataController DataController { get; }

        public void Init(ITeamable teamable, ISpawnPoint spawnData);

        public void SelectActorData(IActorData actorData);

        public void CancelSpawnActor(IActorData actorData);
    }
}