using BattleBase.Gameplay.Actors.AI.State;
using BattleBase.Gameplay.Actors.AttackSystem;
using System;

namespace BattleBase.Gameplay.Actors.AI.Transition
{
    public class HunterStateTransition : IStateTransition
    {
        private readonly HunterState _hunterState;
        private readonly IAttackNotifier _attackNotifier;

        public event Action<IActorState> StateChanged;

        public HunterStateTransition(HunterState hunterState, IAttackNotifier attackNotifier)
        {
            _hunterState = hunterState ?? throw new ArgumentNullException(nameof(hunterState));
            _attackNotifier = attackNotifier ?? throw new ArgumentNullException(nameof(attackNotifier));
        }

        public void Enable()
        {
            _attackNotifier.TargetSelected += OnSetHunterState;
        }

        public void Disable()
        {
            _attackNotifier.TargetSelected -= OnSetHunterState;
        }

        private void OnSetHunterState()
        {
            StateChanged?.Invoke(_hunterState);
        }
    }
}