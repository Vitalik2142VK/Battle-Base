using BattleBase.Gameplay.Actors.AttackSystem;
using BattleBase.Gameplay.Actors.Movement.Hunt;
using System;

namespace BattleBase.Gameplay.Actors.AI.State
{
    public class HunterState : IActorState
    {
        private readonly IAttacker _attacker;
        private readonly IHuntMover _huntMover;

        public HunterState(IAttacker attacker, IHuntMover huntMover)
        {
            _attacker = attacker ?? throw new ArgumentException(nameof(attacker));
            _huntMover = huntMover ?? throw new ArgumentException(nameof(huntMover));
        }

        public void Enter()
        {
            _attacker.SetAttacking(true);
            _huntMover.EstablishTarget(_attacker.CurrentTarget);
            _huntMover.Move();
        }

        public void Exit()
        {
            _attacker.SetAttacking(false); 
            _huntMover.ResetTarget();
        }
    }
}
