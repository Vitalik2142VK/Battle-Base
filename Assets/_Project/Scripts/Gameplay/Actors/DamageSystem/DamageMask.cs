using System;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    [Flags]
    public enum DamageMask
    {
        None = 0,
        ArmorPiercing = 1 << 0,
    }
}