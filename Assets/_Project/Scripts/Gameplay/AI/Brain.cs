using BattleBase.Core;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI
{
    public class Brain : IBrain
    {
        private readonly List<ITactic> _tactics;
        private readonly IBrainConfing _confing;
        private readonly ITacticsFactory _factory;

        public Brain(IBrainConfing confing, ITacticsFactory factory)
        {
            _confing = confing ?? throw new ArgumentNullException(nameof(confing));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));

            _tactics = new List<ITactic>();
        }

        public void Init()
        {
            IEnumerable<ITactic> tactics = _factory.Create(_confing);

            foreach (var tactic in tactics)
                _tactics.Add(tactic);
        }

        public bool TryGetCommand(out ICommand command)
        {
            command = null;

            foreach (var tactic in _tactics)
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