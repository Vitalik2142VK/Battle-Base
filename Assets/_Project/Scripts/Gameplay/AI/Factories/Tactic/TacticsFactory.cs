using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.AI.Deactevators;
using BattleBase.Gameplay.AI.Factories.Deactevator;
using BattleBase.Gameplay.AI.Tactics;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Factories.Tactic
{
    public class TacticsFactory : ITacticsFactory
    {
        private readonly List<ITacticFactory> _factories;
        private readonly ITacticDeactivatorFactory _tacticDeactevatorFactory;

        public TacticsFactory(
            IEnumerable<ITacticFactory> tacticFactories,
            ITacticDeactivatorFactory tacticDeactevatorFactory)
        {
            if (tacticFactories == null)
                throw new ArgumentNullException(nameof(tacticFactories));

            _tacticDeactevatorFactory = tacticDeactevatorFactory ?? throw new ArgumentNullException(nameof(tacticDeactevatorFactory));

            _factories = new List<ITacticFactory>(tacticFactories);
        }

        public IEnumerable<ITactic> Create(IBrainConfig cofing)
        {
            if (cofing == null)
                throw new ArgumentNullException(nameof(cofing));

            List<ITactic> tactics = new();

            foreach (var setting in cofing.TacticSetting)
            {
                if (TryCreateTactic(setting, cofing.TeamType, out ITactic tactic))
                    tactics.Add(tactic);
            }

            return tactics;
        }

        private bool TryCreateTactic(ITacticSetting setting, TeamType team, out ITactic tactic)
        {
            tactic = null;

            foreach (var factory in _factories)
            {
                if (factory.TryCreate(setting, team, out tactic))
                {
                    ITacticDeactivator tacticDeactivator = _tacticDeactevatorFactory.Create(setting.DeactivatorScores);
                    tactic.Init(tacticDeactivator);

                    return true;
                }
            }

            return false;
        }
    }
}