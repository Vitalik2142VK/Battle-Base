using System;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IAttackStateEvent
    {
        public event Action AttackActivated;

        public event Action AttackDeactivated;
    }
}