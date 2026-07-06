namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawnService
    {
        public Actor Spawn(string prefabName, ISpawnPoint spawnData);
    }
}