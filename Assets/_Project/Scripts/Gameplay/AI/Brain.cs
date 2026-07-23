using BattleBase.Core;
using BattleBase.Gameplay.Actors;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI
{
    public class Brain : IBrain
    {
        private readonly Dictionary<TacticType, ITactic> _tactics;
        private readonly TeamType _team;

        public Brain(IEnumerable<ITactic> tactics, IBrainConfing confing)
        {
            if (tactics == null)
                throw new ArgumentNullException(nameof(tactics));

            if (confing == null)
                throw new ArgumentNullException(nameof(confing));

            _team = confing.TeamType;
            _tactics = new Dictionary<TacticType, ITactic>();

            foreach (var tacticType in confing.UsedTacticTypes)
                _tactics.Add(tacticType, null);

            foreach (var tactic in tactics)
            {
                if (_tactics.ContainsKey(tactic.Type))
                    _tactics[tactic.Type] = tactic;
            }
        }

        public bool TryGetCommand(out ICommand command)
        {
            command = null;

            foreach (var tactic in _tactics.Values)
            {
                tactic.SetTeamm(_team);

                if (tactic.CanAction())
                {
                    command = tactic.GetCommand();

                    return true;
                }
            }

            return false;
        }
    }
}