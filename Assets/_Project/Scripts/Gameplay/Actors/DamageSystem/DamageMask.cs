using System;

namespace BattleBase.Gameplay.Actors
{
    [Flags]
    public enum DamageMask
    {
        [Obsolete("Empty mask is not allowed", true)]
        None = 0,

        Building = 1 << 0,
        Infantry = 1 << 1,
        LightVehicle = 1 << 2,
        HeavyVehicle = 1 << 3,
        Air = 1 << 4
    }
}
