using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.Production;
using System;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class MaterialCreatorImprover : IMaterialCreatorImprover
    {
        private readonly IImprover _improver;
        private readonly IMaterialCreator _materialCreator;
        private readonly IMaterialRegistry _materialRegistry;
        private readonly ITeamable _teamable;

        public MaterialCreatorImprover(
            IMaterialCreator materialCreator, 
            IImprover improvement,
            IMaterialRegistry materialRegistry,
            ITeamable teamable)
        {
            _materialCreator = materialCreator ?? throw new ArgumentNullException(nameof(materialCreator));
            _improver = improvement ?? throw new ArgumentNullException(nameof(improvement));
            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));
            _teamable = teamable ?? throw new ArgumentNullException(nameof(teamable));
        }

        public IProductionData Data => _improver.Data;

        public bool CanImprove => _materialCreator.CanIncreaseProduction && _improver.CanImprove;

        public void Enable() =>
            _improver.Enable();

        public void Disable() =>
            _improver.Disable();

        public void Improve()
        {
            if (_materialCreator.CanIncreaseProduction == false)
                return;

            if (_materialRegistry.TrySpend(_teamable.TeamType, _improver.Data.Price) == false)
                return;

            _materialCreator.IncreaseProduction();
            _improver.Improve();
        }
    }
}