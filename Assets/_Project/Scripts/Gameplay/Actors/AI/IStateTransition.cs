using System;

namespace BattleBase.Gameplay.Actors.AI
{
    public interface IStateTransition
    {
        public event Action<IActorState> StateChanged;

        public void Enable();

        public void Disable();
    }
}