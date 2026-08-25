using BattleBase.Gameplay.Actors.AI.State;
using BattleBase.Gameplay.Actors.AttackSystem;
using System;

namespace BattleBase.Gameplay.Actors.AI.Transition
{
    public class AttackStateTransition : IStateTransition
    {
        private readonly AttackState _attackState;
        private readonly IAttackStateEvent _attackEvent;

        public event Action<IActorState> StateChanged;

        public AttackStateTransition(AttackState attackState, IAttackStateEvent attackEvent)
        {
            _attackState = attackState ?? throw new ArgumentNullException(nameof(attackState));
            _attackEvent = attackEvent ?? throw new ArgumentNullException(nameof(attackEvent));
        }

        public void Enable()
        {
            _attackEvent.AttackActivated += OnSetAttackState;
        }

        public void Disable()
        {
            _attackEvent.AttackActivated -= OnSetAttackState;
        }

        private void OnSetAttackState()
        {
            StateChanged?.Invoke(_attackState);
        }
    }
}