using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.AI.Modifiers;
using BattleBase.Gameplay.AI.Modifiers.Defense;
using System;

namespace BattleBase.Gameplay.AI.Factories
{
    public class DefenseModifierFactory : IScoreModifierFactory
    {
        private readonly IAreaDefenseAI _areaDefenseAI;

        public DefenseModifierFactory(IAreaDefenseAI areaDefenseAI)
        {
            _areaDefenseAI = areaDefenseAI ?? throw new ArgumentNullException(nameof(areaDefenseAI));
        }

        public ModifierType Type => ModifierType.Defense;

        public IAdvancedScoreModifier Create(IScoreModifierConfig configs, TeamType team)
        {
            if (configs == null)
                throw new ArgumentNullException(nameof(configs));

            if (configs is IDefenseModifierConfig defenseModifierConfig == false)
                throw new InvalidOperationException($"{nameof(configs)} is not implemented '{nameof(IDefenseModifierConfig)}'");

            return new DefenseScoreModifier(defenseModifierConfig, _areaDefenseAI);
        }
    }
}