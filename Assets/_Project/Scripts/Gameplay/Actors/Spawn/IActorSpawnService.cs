namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawnService
    {
        bool TrySpawn(string prefabName, out Actor actor);
    }
}