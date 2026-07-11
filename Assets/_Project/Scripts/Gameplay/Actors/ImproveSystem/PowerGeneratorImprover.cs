using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.Energy;
using BattleBase.Gameplay.Actors.Production;
using System;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class PowerGeneratorImprover : IPowerGeneratorImprover
    {
        private readonly IImprover _improver;
        private readonly IPowerGenerator _powerGenerator;
        private readonly IMaterialRegistry _materialRegistry;
        private readonly ITeamable _teamable;

        public PowerGeneratorImprover(
            IPowerGenerator powerGenerator, 
            IImprover improvement, 
            IMaterialRegistry materialRegistry,
            ITeamable teamable)
        {
            _powerGenerator = powerGenerator ?? throw new ArgumentNullException(nameof(powerGenerator));
            _improver = improvement ?? throw new ArgumentNullException(nameof(improvement));
            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));
            _teamable = teamable ?? throw new ArgumentNullException(nameof(teamable));
        }

        public IProductionData Data => _improver.Data;

        public bool CanImprove => _powerGenerator.CanIncreasePower && _improver.CanImprove;

        public void Enable() =>
            _improver.Enable();

        public void Disable() =>
            _improver.Disable();

        public void Improve()
        {
            if (_powerGenerator.CanIncreasePower == false)
                return;

            if (_materialRegistry.TrySpend(_teamable.TeamType, _improver.Data.Price) == false)
                return;

            _powerGenerator.IncreasePower();
            _improver.Improve();
        }
    }
}