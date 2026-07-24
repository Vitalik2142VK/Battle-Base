using BattleBase.Gameplay.Actors.Energy;
using BattleBase.Gameplay.Actors.Production.Improve;
using System;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class PowerGeneratorImprover : IPowerGeneratorImprover
    {
        private readonly IImprover _improver;
        private readonly IPowerGenerator _powerGenerator;

        public PowerGeneratorImprover(
            IPowerGenerator powerGenerator, 
            IImprover improvement)
        {
            _powerGenerator = powerGenerator ?? throw new ArgumentNullException(nameof(powerGenerator));
            _improver = improvement ?? throw new ArgumentNullException(nameof(improvement));
        }

        public IImproveProductionData Data => _improver.Data;

        public bool CanImprove => _powerGenerator.CanIncreasePower && _improver.CanImprove;

        public void Enable() =>
            _improver.Enable();

        public void Disable() =>
            _improver.Disable();

        public bool TryImprove()
        {
            if (CanImprove == false)
                return false;

            if (_improver.TryImprove())
            {
                _powerGenerator.IncreasePower();

                return true;
            }

            return false;
        }
    }
}