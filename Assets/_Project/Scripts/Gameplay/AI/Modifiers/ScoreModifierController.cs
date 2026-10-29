using BattleBase.Gameplay.AI.Factories.Modifier;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Modifiers
{
    public class ScoreModifierController
    {
        private readonly List<IAdvancedScoreModifier> _modifiers;
        private readonly DefaultScoreModifier _defaultModifier;

        public ScoreModifierController(IScoreModifiersFactory factory, IBrainConfig brainConfing)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            if (brainConfing == null)
                throw new ArgumentNullException(nameof(brainConfing));

            IEnumerable<IAdvancedScoreModifier> modifiers = factory.Create(brainConfing);
            _modifiers = new List<IAdvancedScoreModifier>(modifiers);
            _defaultModifier = new DefaultScoreModifier();
        }

        public IScoreModifier GetPriorityModifier()
        {
            IScoreModifier scoreModifier = _defaultModifier;
            int currentModifierPriority = int.MaxValue;

            foreach (var modifier in _modifiers)
            {
                if (modifier.IsActivationNecessary())
                {
                    if (currentModifierPriority > modifier.Priority)
                    {
                        currentModifierPriority = modifier.Priority;
                        scoreModifier = modifier;
                    }
                }

                if (currentModifierPriority == 1)
                    break;
            }

            return scoreModifier;
        }
    }
}