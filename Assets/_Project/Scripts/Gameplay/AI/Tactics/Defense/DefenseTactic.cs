using BattleBase.Core;
using BattleBase.Gameplay.Actors.Building;
using System;

namespace BattleBase.Gameplay.AI.Tactics.Defense
{
    public class DefenseTactic : ITactic, IDisposable
    {
        public DefenseTactic(
            ITacticTool tool,
            IBuildingSitesController controller,
            IDefenseTacticSetting setting)
        {
            throw new NotImplementedException();
        }

        public TacticCategory Category =>
            throw new NotImplementedException();

        public int Score =>
            throw new NotImplementedException();

        public bool CanAction =>
            throw new NotImplementedException();

        public void CalculateScore()
        {
            throw new NotImplementedException();
        }

        public ICommand GetCommand()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}