using BattleBase.Gameplay.Actors.AttackSystem;
using System;

namespace BattleBase.Gameplay.Actors.AI.Activated
{
    public class AttackActivatedState : IActivatedState
    {
        private readonly IAttackNotifier _attackNotifier;
        private readonly IActorState _actorState;

        public AttackActivatedState(IAttackNotifier attackNotifier, IActorState actorState)
        {
            _attackNotifier = attackNotifier ?? throw new ArgumentNullException(nameof(attackNotifier));
            _actorState = actorState ?? throw new ArgumentNullException(nameof(actorState));
        }

        public IActorState ActorState => _actorState;

        public bool CanAction => _attackNotifier.HasTarget;
    }
}
