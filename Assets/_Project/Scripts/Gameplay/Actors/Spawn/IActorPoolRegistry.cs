namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorPoolRegistry
    {
        public bool TryGive(out Actor actor, string namePrefab);
    }
}