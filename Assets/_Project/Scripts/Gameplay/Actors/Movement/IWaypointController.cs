using BattleBase.Gameplay.Actors.Spawn;

namespace BattleBase.Gameplay.Actors.Movement
{
    public interface IWaypointController
    {
        public void SpecifyActorRoute(IMover mover, ISpawnPoint spawnData);
    }
}