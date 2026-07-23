using BattleBase.Gameplay.Actors.DamageSystem;
using System;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IAttackNotifier : IAttackStateEvent
    {
        public event Action TargetSelected;

        public event Action Attacked;

        public ITarget CurrentTarget { get; }
    }
}