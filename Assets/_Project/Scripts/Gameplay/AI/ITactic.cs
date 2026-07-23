using BattleBase.Core;
using BattleBase.Gameplay.Actors;

namespace BattleBase.Gameplay.AI
{
    public interface ITactic
    {
        public TacticType Type { get; }

        public bool CanAction();

        public void SetTeamm(TeamType teamType);

        public ICommand GetCommand();
    }
}