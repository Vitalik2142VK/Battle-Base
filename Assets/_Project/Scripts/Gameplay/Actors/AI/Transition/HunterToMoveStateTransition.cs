using BattleBase.Gameplay.Actors.AI.State;
using BattleBase.Gameplay.Actors.AttackSystem;
using System;

namespace BattleBase.Gameplay.Actors.AI.Transition
{
    public class HunterToMoveStateTransition : IStateTransition //todo not used
    {
        private readonly MoveState _moveState;
        private readonly IAttackStateEvent _attackEvent;

        public event Action<IActorState> StateChanged;

        public HunterToMoveStateTransition(MoveState moveState, IAttackStateEvent attackEvent)
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
            StateChanged?.Invoke(_moveState);
        }
    }
}