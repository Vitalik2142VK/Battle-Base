using BattleBase.Gameplay.Actors.AttackSystem;

namespace BattleBase.Gameplay.Actors.AI.State
{
    public class AttackState : IActorState
    {
        public readonly IAttacker _attacker;

        public AttackState(IAttacker attacker)
        {
            _attacker = attacker ?? throw new System.ArgumentNullException(nameof(attacker));
        }

        public void Enter()
        {
            _attacker.SetAttacking(true);
        }

        public void Exit()
        {
            _attacker.SetAttacking(false);
        }
    }
}