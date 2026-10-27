using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.AI.Deactevators;
using System;

namespace BattleBase.Gameplay.AI.Factories.Deactevator
{
    public class DeactivatorByEconomicFactory : IDeactivatorFactory
    {
        private readonly IMaterialRegistry _materialRegistry;

        public DeactivatorByEconomicFactory(IMaterialRegistry materialRegistry)
        {
            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));
        }

        public Type DeactevatorType => typeof(DeactivatorByEconomicScore);

        public IDeactivator Create(ITacticDeactivatorScore score)
        {
            if (score == null)
                throw new ArgumentNullException(nameof(score));

            if (score is DeactivatorByEconomicScore economicDeactivatorScore == false)
                throw new InvalidOperationException($"{nameof(score)} does not implement {nameof(DeactivatorByEconomicScore)}");

            IMaterialData materialData = _materialRegistry.GetMaterialData(economicDeactivatorScore.TeamType);
            int materialsForDeactivate = economicDeactivatorScore.MaterialsForDeactivate;

            return new DeactivatorByEconomic(materialData, materialsForDeactivate);
        }
    }
}