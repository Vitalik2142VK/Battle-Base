namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawnService
    {
        bool TrySpawn(string prefabName, ISpawnPoint spawnData, out Actor actor);
    }
}