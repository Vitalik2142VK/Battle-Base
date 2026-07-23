using BattleBase.Core;
using BattleBase.Gameplay.Actors;
using System;

namespace BattleBase.Gameplay.AI.TacticTypes
{
    public partial class TankTestTactic : ITactic
    {
        public TacticType Type => throw new NotImplementedException();

        public bool CanAction()
        {
            throw new NotImplementedException();
        }

        public ICommand GetCommand()
        {
            throw new NotImplementedException();
        }

        public void SetTeamm(TeamType teamType)
        {
            throw new NotImplementedException();
        }
    }
}