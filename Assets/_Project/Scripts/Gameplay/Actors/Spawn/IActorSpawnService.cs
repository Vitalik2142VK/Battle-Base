namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawnService
    {
        bool TrySpawn(string prefabName, TeamType teamType, out Actor actor);
    }
}