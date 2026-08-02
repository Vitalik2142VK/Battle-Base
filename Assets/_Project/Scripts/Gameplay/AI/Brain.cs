using BattleBase.Core;
using BattleBase.Gameplay.AI.Modifiers;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI
{
    public class Brain : IBrain
    {
        private readonly List<ITactic> _tactics;
        private readonly IBrainConfing _confing;
        private readonly ITacticsFactory _tacticsFactory;
        private readonly IScoreModifiersFactory _modifiersFactory;

        private ScoreModifierController _modifierController;
        private int _currentIndexTactic;

        public Brain(IBrainConfing confing, ITacticsFactory tacticsFactory, IScoreModifiersFactory modifiersFactory)
        {
            _confing = confing ?? throw new ArgumentNullException(nameof(confing));
            _tacticsFactory = tacticsFactory ?? throw new ArgumentNullException(nameof(tacticsFactory));
            _modifiersFactory = modifiersFactory ?? throw new ArgumentNullException(nameof(modifiersFactory));

            _tactics = new List<ITactic>();
            _currentIndexTactic = 0;
        }

        public bool ThinkCompleted => _currentIndexTactic >= _tactics.Count;

        public void Init()
        {
            IEnumerable<ITactic> tactics = _tacticsFactory.Create(_confing);

            foreach (var tactic in tactics)
                _tactics.Add(tactic);

            _modifierController = new ScoreModifierController(_modifiersFactory, _confing);
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
            IScoreModifier modifier = _modifierController.GetPriorityModifier();
            ITactic selectedTactic = null;
            int currentTacticScore = 0;
            command = null;

            foreach (var tactic in _tactics)
            {
                if (tactic.Score <= 0)
                    continue;

                if (selectedTactic == null)
                {
                    selectedTactic = tactic;
                    currentTacticScore = modifier.Modify(tactic.Category, tactic.Score);

                    continue;
                }

                int tacticScore = modifier.Modify(tactic.Category, tactic.Score);

                if (currentTacticScore < tacticScore)
                {
                    selectedTactic = tactic;
                    currentTacticScore = tacticScore;
                }
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