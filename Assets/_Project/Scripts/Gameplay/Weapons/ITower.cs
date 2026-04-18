using System;

namespace BattleBase.Gameplay.Weapons
{
    public interface ITower
    {
        public event Action<bool> Aimed;

        public void TakeAim(ITargetPoint target);
    }
}