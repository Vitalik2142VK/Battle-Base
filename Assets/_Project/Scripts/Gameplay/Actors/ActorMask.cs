using System;

namespace BattleBase.Gameplay.Actors
{
    [Flags]
    public enum ActorMask
    {
        [Obsolete("Empty mask is not allowed", true)]
        None = 0,

        Building = 1 << 0,
        Infantry = 1 << 1,
        Vehicle = 1 << 2,
        Air = 1 << 3
    }
}
