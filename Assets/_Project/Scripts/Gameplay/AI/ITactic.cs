using BattleBase.Core;

namespace BattleBase.Gameplay.AI
{
    public interface ITactic
    {
        public TacticType Type { get; }

        public bool CanAction();

        public ICommand GetCommand();
    }
}