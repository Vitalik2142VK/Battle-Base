using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.AI.Modifiers;
using BattleBase.Gameplay.AI.Modifiers.Economy;
using System;

namespace BattleBase.Gameplay.AI.Factories
{
    public class EconomyModifierFactory : IScoreModifierFactory
    {
        private readonly IMaterialRegistry _materialRegistry;

        public EconomyModifierFactory(IMaterialRegistry materialRegistry)
        {
            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));
        }

        public ModifierType Type => ModifierType.Economy;

        public IAdvancedScoreModifier Create(IScoreModifierConfig configs, TeamType team)
        {
            if (configs == null)
                throw new ArgumentNullException(nameof(configs));

            if (configs is IEconomyModifierConfig economyModifierConfig == false)
                throw new InvalidOperationException($"{nameof(configs)} is not implemented '{nameof(IEconomyModifierConfig)}'");

            IMaterialData materialData = _materialRegistry.GetMaterialData(team);

            return new EconomyScoreModifier(economyModifierConfig, materialData);
        }
    }
}