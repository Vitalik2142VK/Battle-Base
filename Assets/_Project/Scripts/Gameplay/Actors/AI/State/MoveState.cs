using BattleBase.Gameplay.Actors.Movement;

namespace BattleBase.Gameplay.Actors.AI.State
{
    public class MoveState : IActorState
    {
        public readonly IMover _mover;

        public MoveState(IMover mover)
        {
            _mover = mover ?? throw new System.ArgumentNullException(nameof(mover));
        }

        public void Enter()
        {
            _mover.Move();
        }

        public void Exit()
        {
            _mover.Stop();
        }

        public void Update(float _)
        {
            if (_mover.CanMove == false)
                _mover.Stop();
        }
    }
}
