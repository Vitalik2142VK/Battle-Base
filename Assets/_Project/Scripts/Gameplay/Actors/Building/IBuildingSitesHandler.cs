using BattleBase.Gameplay.Actors.Spawn;

namespace BattleBase.Gameplay.Actors.Building
{
    public interface IBuildingSitesHandler
    {
        public void Register(IBuildingSite buildingSite, IActorSpawnerEvents events);
    }
}