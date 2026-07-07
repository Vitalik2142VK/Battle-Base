namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawnService
    {
        public Actor Spawn(string id, TeamType teamType, ISpawnPoint spawnData);
    }
}