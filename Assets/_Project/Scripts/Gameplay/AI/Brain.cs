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

        private int _currentIndexTactic;

        public Brain(IBrainConfing confing, ITacticsFactory factory)
        {
            _confing = confing ?? throw new ArgumentNullException(nameof(confing));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));

            _tactics = new List<ITactic>();
            _currentIndexTactic = 0;
        }

        public bool ThinkCompleted => _currentIndexTactic >= _tactics.Count;

        public void Init()
        {
            IEnumerable<ITactic> tactics = _factory.Create(_confing);

            foreach (var tactic in tactics)
                _tactics.Add(tactic);
        }

        public void ThinkDuringTick()
        {
            if (ThinkCompleted)
                return;

            _tactics[_currentIndexTactic++].CalculateScore();
        }

        public bool TryGetCommand(out ICommand command)
        {
            _currentIndexTactic = 0;
            ITactic selectedTactic = null;
            command = null;

            foreach (var tactic in _tactics)
            {
                if (tactic.Score <= 0)
                    continue;

                if (selectedTactic == null)
                {
                    selectedTactic = tactic;

                    continue;
                }

                if (selectedTactic.Score < tactic.Score)
                    selectedTactic = tactic;
            }

            if (selectedTactic != null && selectedTactic.CanAction)
            {
                command = selectedTactic.GetCommand();

                return true;
            }

            return false;
        }
    }
}