namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public static class DamageMaskExtensions
    {
        public static bool Contains(this DamageMask mask, DamageMask value)
            => (mask & value) == value;

        public static bool ContainsAny(this DamageMask mask, DamageMask value)
            => (mask & value) != 0;
    }
}