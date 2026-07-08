using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.Production;
using System;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class MaterialCreatorImprover : IMaterialCreatorImprover
    {
        private readonly IImprover _improvement;
        private readonly IMaterialCreator _materialCreator;

        public MaterialCreatorImprover(IMaterialCreator materialCreator, IImprover improvement)
        {
            _materialCreator = materialCreator ?? throw new ArgumentNullException(nameof(materialCreator));
            _improvement = improvement ?? throw new ArgumentNullException(nameof(improvement));
        }

        public IImproverData Data => _improvement.Data;

        public bool CanImprove => _materialCreator.CanIncreaseProduction;

        public void Init(IProductionData currentData) =>
            _improvement.Init(currentData);

        public void Enable() =>
            _improvement.Enable();

        public void Disable() =>
            _improvement.Disable();

        public void Improve()
        {
            if (_materialCreator.CanIncreaseProduction == false)
                return;

            _materialCreator.IncreaseProduction();
        }
    }
}