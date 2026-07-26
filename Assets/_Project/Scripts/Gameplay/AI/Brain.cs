using BattleBase.Core;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI
{
    public class Brain : IBrain
    {
        private readonly Dictionary<TacticType, ITactic> _tactics;

        public Brain(IBrainConfing confing, ITacticsFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            if (confing == null)
                throw new ArgumentNullException(nameof(confing));

            _tactics = new Dictionary<TacticType, ITactic>();
            IEnumerable<ITactic> tactics = factory.Create(confing);

            foreach (var tactic in tactics)
                _tactics.Add(tactic.Type, tactic);
        }

        public bool TryGetCommand(out ICommand command)
        {
            command = null;

            foreach (var tactic in _tactics.Values)
            {
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