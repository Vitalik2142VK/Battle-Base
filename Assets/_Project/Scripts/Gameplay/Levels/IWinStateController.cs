using BattleBase.Gameplay.Actors;
using System;

namespace BattleBase.Gameplay.Levels
{
    public interface IWinStateController
    {
        public event Action<bool> Winned;

        public void AddBase(Actor actor);
    }
}