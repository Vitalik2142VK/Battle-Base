using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.AI.Deactevators;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics
{
    public class TacticsFactory : ITacticsFactory
    {
        private readonly Dictionary<TacticCategory, List<ITacticFactory>> _tacticFactories;
        private readonly ITacticDeactivatorFactory _tacticDeactevatorFactory;

        public TacticsFactory(
            IEnumerable<ITacticFactory> tacticFactories, 
            ITacticDeactivatorFactory tacticDeactevatorFactory)
        {
            if (tacticFactories == null)
                throw new ArgumentNullException(nameof(tacticFactories));

            _tacticDeactevatorFactory = tacticDeactevatorFactory ?? throw new ArgumentNullException(nameof(tacticDeactevatorFactory));

            _tacticFactories = new Dictionary<TacticCategory, List<ITacticFactory>>();

            foreach (var factory in tacticFactories)
            {
                TacticCategory category = factory.Category;

                if (_tacticFactories.ContainsKey(category) == false)
                    _tacticFactories.Add(category, new List<ITacticFactory>());

                _tacticFactories[category].Add(factory);
            }
        }

        public IEnumerable<ITactic> Create(IBrainConfig cofing)
        {
            if (cofing == null)
                throw new ArgumentNullException(nameof(cofing));

            List<ITactic> tactics = new();

            foreach (var setting in cofing.TacticSetting)
            {
                if (_tacticFactories.TryGetValue(setting.Category, out List<ITacticFactory> factories) == false)
                    throw new InvalidOperationException($"{nameof(_tacticFactories)} not contains {nameof(TacticCategory)} '{setting.Category}'");

                ITactic tactic = CreateTactic(factories, setting, cofing.TeamType);
                tactics.Add(tactic);
            }

            return tactics;
        }

        private ITactic CreateTactic(List<ITacticFactory> factories, ITacticSetting setting, TeamType team)
        {
            foreach (var factory in factories)
            {
                if (factory.TryCreate(setting, team, out ITactic tactic))
                {
                    ITacticDeactivator tacticDeactivator = _tacticDeactevatorFactory.Create(setting.DeactivatorScores);
                    tactic.Init(tacticDeactivator);

                    return tactic;
                }
            }

            throw new InvalidOperationException($"There is no suitable factory for setting '{setting.Category}'");
        }
    }
}