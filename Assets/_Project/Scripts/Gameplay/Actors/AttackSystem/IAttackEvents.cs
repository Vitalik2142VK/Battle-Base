using BattleBase.Gameplay.Actors.DamageSystem;
using System;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IAttackEvents : IAttackStateEvent
    {
        public event Action<ITarget> TargetSelected;

        public event Action Attacked;
    }
}