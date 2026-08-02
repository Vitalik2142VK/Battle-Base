using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Modifiers
{
    public class ScoreModifierController
    {
        private readonly List<IAdvancedScoreModifier> _modifiers;
        private readonly DefaultScoreModifier _defaultModifier;

        public ScoreModifierController(IScoreModifiersFactory factory, IBrainConfing brainConfing)
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
            foreach (var modifier in _modifiers)
            {
                if (modifier.IsActivationNecessary())
                    return modifier;
            }

            return _defaultModifier;
        }
    }
}