using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Modifiers
{
    public class ScoreModifiersFactory : IScoreModifiersFactory
    {
        private readonly Dictionary<ModifierType, IScoreModifierFactory> _factories;

        public ScoreModifiersFactory(IEnumerable<IScoreModifierFactory> factories)
        {
            if (factories == null)
                throw new ArgumentNullException(nameof(factories));

            _factories = new Dictionary<ModifierType, IScoreModifierFactory>();

            foreach (var factory in factories)
                _factories.Add(factory.Type, factory);
        }

        public IEnumerable<IAdvancedScoreModifier> Create(IBrainConfing cofing)
        {
            List<IAdvancedScoreModifier> scoreModifiers = new();

            foreach (var modifierConfig in cofing.ScoreModifierConfigs)
            {
                if (_factories.TryGetValue(modifierConfig.Type, out IScoreModifierFactory factory) == false)
                    throw new InvalidOperationException($"{nameof(_factories)} not contains {nameof(ModifierType)} '{modifierConfig.Type}'");

                IAdvancedScoreModifier scoreModifier = factory.Create(modifierConfig, cofing.TeamType);
                scoreModifiers.Add(scoreModifier);
            }

            return scoreModifiers;
        }
    }
}