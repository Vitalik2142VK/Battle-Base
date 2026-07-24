using System;

namespace BattleBase.Gameplay.Actors
{
    public interface IActorComponent
    {
        public Type KeyType { get; }

        public void Enable();

        public void Disable();
    }
}
