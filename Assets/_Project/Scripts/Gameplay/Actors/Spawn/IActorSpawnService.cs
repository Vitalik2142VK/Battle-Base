namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawnService
    {
        bool TrySpawn(string prefabName, ISpawnData spawnData, out Actor actor);
    }
}