using BattleBase.Gameplay.Actors.DamageSystem;
using System;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IAttackNotifier : IAttackStateEvent
    {
        public event Action TargetSelected;
        public event Action TargetLosted;
        public event Action Attacked;
        public event Action SearchingTarget;

        public ITarget CurrentTarget { get; }

        public bool HasTarget { get; }
    }
}