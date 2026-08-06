using BattleBase.Gameplay.Actors;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics
{
    public class TacticsFactory : ITacticsFactory
    {
        private readonly IEnumerable<ITacticFactory> _factories;

        public TacticsFactory(IEnumerable<ITacticFactory> factories)
        {
            _factories = factories ?? throw new ArgumentNullException(nameof(factories));
        }

        public IEnumerable<ITactic> Create(IBrainConfing cofing)
        {
            if (cofing == null)
                throw new ArgumentNullException(nameof(cofing));

            List<ITacticFactory> factories = new(_factories);
            List<ITactic> tactics = new();

            foreach (var setting in cofing.TacticSetting)
            {
                ITactic tactic = CreateTactic(factories, setting, cofing.TeamType);
                tactics.Add(tactic);
            }

            return tactics;
        }

        private ITactic CreateTactic(List<ITacticFactory> factories, ITacticSetting setting, TeamType team)
        {
            for (int i = 0; i < factories.Count; i++)
            {
                ITacticFactory factory = factories[i];

                if (factory.TryCreate(setting, team, out ITactic tactic))
                {
                    factories.RemoveAt(i);

                    return tactic;
                }
            }

            throw new InvalidOperationException($"There is no suitable factory for setting '{nameof(setting)}'");
        }
    }
}