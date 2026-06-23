using BattleBase.Gameplay.Actors.Spawn;

namespace BattleBase.Gameplay.Actors.Building
{
    public interface IRegisteredBuildingSite
    {
        public bool TryGetActorSpawner(out IActorSpawner spawner);
    }
}