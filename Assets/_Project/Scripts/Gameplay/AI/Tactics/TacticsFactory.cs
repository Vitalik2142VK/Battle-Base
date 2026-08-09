using BattleBase.Gameplay.Actors;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics
{
    public class TacticsFactory : ITacticsFactory
    {
        private readonly Dictionary<TacticCategory, List<ITacticFactory>> _factories;

        public TacticsFactory(IEnumerable<ITacticFactory> factories)
        {
            if (factories == null)
                throw new ArgumentNullException(nameof(factories));

            _factories = new Dictionary<TacticCategory, List<ITacticFactory>>();

            foreach (var factory in factories)
            {
                TacticCategory category = factory.Category;

                if (_factories.ContainsKey(category) == false)
                    _factories.Add(category, new List<ITacticFactory>());

                _factories[category].Add(factory);
            }
        }

        public IEnumerable<ITactic> Create(IBrainConfing cofing)
        {
            if (cofing == null)
                throw new ArgumentNullException(nameof(cofing));

            List<ITactic> tactics = new();

            foreach (var setting in cofing.TacticSetting)
            {
                if (_factories.TryGetValue(setting.Category, out List<ITacticFactory> factories) == false)
                    throw new InvalidOperationException($"{nameof(_factories)} not contains {nameof(TacticCategory)} '{setting.Category}'");

                ITactic tactic = CreateTactic(factories, setting, cofing.TeamType);
                tactics.Add(tactic);
            }

            return tactics;
        }

        private ITactic CreateTactic(List<ITacticFactory> factories, ITacticSetting setting, TeamType team)
        {
            foreach (ITacticFactory factory in factories)
            {
                if (factory.TryCreate(setting, team, out ITactic tactic))
                    return tactic;
            }

            throw new InvalidOperationException($"There is no suitable factory for setting '{setting.Category}'");
        }
    }
}