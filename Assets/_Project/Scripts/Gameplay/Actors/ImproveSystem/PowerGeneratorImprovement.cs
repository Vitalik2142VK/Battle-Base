using BattleBase.Gameplay.Actors.Energy;
using BattleBase.Gameplay.Actors.Production;
using System;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class PowerGeneratorImprovement : IPowerGeneratorImprovement
    {
        private readonly IImprovement _improvement;
        private readonly IPowerGenerator _powerGenerator;

        public PowerGeneratorImprovement(IPowerGenerator powerGenerator, IImprovement improvement)
        {
            _powerGenerator = powerGenerator ?? throw new ArgumentNullException(nameof(powerGenerator));
            _improvement = improvement ?? throw new ArgumentNullException(nameof(improvement));
        }

        public IImprovementData Data => _improvement.Data;

        public bool CanImprove => _powerGenerator.CanIncreasePower;

        public void Init(IProductionData currentData) =>
            _improvement.Init(currentData);

        public void Enable() =>
            _improvement.Enable();

        public void Disable() =>
            _improvement.Disable();

        public void Improve()
        {
            if (_powerGenerator.CanIncreasePower == false)
                return;

            _powerGenerator.IncreasePower();
        }
    }
}