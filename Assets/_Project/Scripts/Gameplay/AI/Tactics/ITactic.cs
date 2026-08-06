using BattleBase.Core;

namespace BattleBase.Gameplay.AI.Tactics
{
    public interface ITactic
    {
        public TacticCategory Category { get; }

        public int Score { get; }

        public bool CanAction { get; }

        public void CalculateScore();

        public ICommand GetCommand();
    }
}