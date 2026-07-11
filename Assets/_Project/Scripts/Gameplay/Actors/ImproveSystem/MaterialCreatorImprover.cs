using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.Production;
using System;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class MaterialCreatorImprover : IMaterialCreatorImprover
    {
        private readonly IImprover _improver;
        private readonly IMaterialCreator _materialCreator;

        public MaterialCreatorImprover(
            IMaterialCreator materialCreator, 
            IImprover improvement)
        {
            _materialCreator = materialCreator ?? throw new ArgumentNullException(nameof(materialCreator));
            _improver = improvement ?? throw new ArgumentNullException(nameof(improvement));
        }

        public IProductionData Data => _improver.Data;

        public bool CanImprove => _materialCreator.CanIncreaseProduction && _improver.CanImprove;

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
                _materialCreator.IncreaseProduction();

                return true;
            }

            return false;
        }
    }
}