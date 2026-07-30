using BattleBase.Core;

namespace BattleBase.Gameplay.AI
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