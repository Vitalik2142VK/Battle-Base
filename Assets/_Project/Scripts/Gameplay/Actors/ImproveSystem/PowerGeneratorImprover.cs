using BattleBase.Gameplay.Actors.Energy;
using BattleBase.Gameplay.Actors.Production;
using System;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class PowerGeneratorImprover : IPowerGeneratorImprover
    {
        private readonly IImprover _improvement;
        private readonly IPowerGenerator _powerGenerator;

        public PowerGeneratorImprover(IPowerGenerator powerGenerator, IImprover improvement)
        {
            _powerGenerator = powerGenerator ?? throw new ArgumentNullException(nameof(powerGenerator));
            _improvement = improvement ?? throw new ArgumentNullException(nameof(improvement));
        }

        public IImproverData Data => _improvement.Data;

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