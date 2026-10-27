using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.AI.Modifiers;
using BattleBase.Gameplay.AI.Modifiers.Univarsal;
using System;

namespace BattleBase.Gameplay.AI.Factories.Modifier
{
    public class TickScoreModifierFactory : IScoreModifierFactory
    {
        public ModifierType Type => ModifierType.Tick;

        public IAdvancedScoreModifier Create(IScoreModifierConfig configs, TeamType team)
        {
            if (configs == null)
                throw new ArgumentNullException(nameof(configs));

            if (configs is ITickScoreModifierConfig tickScoreModifierConfig == false)
                throw new InvalidOperationException($"{nameof(configs)} is not implemented '{nameof(ITickScoreModifierConfig)}'");

            return new TickScoreModifier(tickScoreModifierConfig);
        }
    }
}