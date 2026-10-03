using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.AI.Deactevators;
using System;

namespace BattleBase.Gameplay.AI.Factories
{
    public class EconomicDeactivatorFactory : IDeactivatorFactory
    {
        private readonly IMaterialRegistry _materialRegistry;

        public EconomicDeactivatorFactory(IMaterialRegistry materialRegistry)
        {
            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));
        }

        public Type DeactevatorType => typeof(EconomicDeactivatorScore);

        public IDeactivator Create(ITacticDeactivatorScore score)
        {
            if (score == null)
                throw new ArgumentNullException(nameof(score));

            if (score is EconomicDeactivatorScore economicDeactivatorScore == false)
                throw new InvalidOperationException($"{nameof(score)} does not implement {nameof(EconomicDeactivatorScore)}");

            IMaterialData materialData = _materialRegistry.GetMaterialData(economicDeactivatorScore.TeamType);
            int materialsForDeactivate = economicDeactivatorScore.MaterialsForDeactivate;

            return new EconomicDeactivator(materialData, materialsForDeactivate);
        }
    }
}