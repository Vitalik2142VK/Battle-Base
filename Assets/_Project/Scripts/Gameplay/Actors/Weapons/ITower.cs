using System;

namespace BattleBase.Gameplay.Actors.Weapons
{
    public interface ITower
    {
        public event Action<bool> Aimed;

        public void TakeAim(ITargetPoint target);
    }
}