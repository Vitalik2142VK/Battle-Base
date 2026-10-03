using BattleBase.Core;
using BattleBase.Gameplay.AI.Deactevators;

namespace BattleBase.Gameplay.AI.Tactics
{
    public interface ITactic
    {
        public TacticCategory Category { get; }

        public int Score { get; }

        public bool CanAction { get; }

        public bool IsDeactivated { get; }

        public void Init(ITacticDeactivator tacticDeactivator);

        public void CalculateScore();

        public ICommand GetCommand();
    }
}