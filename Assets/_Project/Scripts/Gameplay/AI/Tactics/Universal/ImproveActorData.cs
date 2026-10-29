using BattleBase.Gameplay.Actors.Building;

namespace BattleBase.Gameplay.AI.Tactics.Universal
{
    public class ImproveActorData
    {
        private int _improveLevel;

        public ImproveActorData(IRegisteredBuildingSite registeredBuilding)
        {
            BuildingSite = registeredBuilding ?? throw new System.ArgumentNullException(nameof(registeredBuilding));

            _improveLevel = 0;
        }

        public IRegisteredBuildingSite BuildingSite { get; }

        public int ImproveLevel => _improveLevel;

        public void IncreaseLevel() =>
            _improveLevel++;
    }
}