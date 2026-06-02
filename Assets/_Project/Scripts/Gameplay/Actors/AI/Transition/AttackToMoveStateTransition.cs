using BattleBase.Gameplay.Actors.AttackSystem;
using System;

namespace BattleBase.Gameplay.Actors.AI.Transition
{
    public class AttackToMoveStateTransition : IStateTransition
    {
        private readonly IActorState _moveState;
        private readonly IAttackStateEvent _attackEvent;

        public event Action<IActorState> StateChanged;

        public AttackToMoveStateTransition(IActorState moveState, IAttackStateEvent attackEvent)
        {
            _moveState = moveState ?? throw new ArgumentNullException(nameof(moveState));
            _attackEvent = attackEvent ?? throw new ArgumentNullException(nameof(attackEvent));
        }

        public void Enable()
        {
            _attackEvent.AttackDeactivated += OnSetMoveState;
        }

        public void Disable()
        {
            _attackEvent.AttackDeactivated -= OnSetMoveState;
        }

        private void OnSetMoveState()
        {
            StateChanged.Invoke(_moveState);
        }
    }
}