namespace BattleBase.Gameplay.Actors
{
    public static class ActorMaskExtensions
    {
        public static bool Contains(this ActorMask mask, ActorMask value)
            => (mask & value) == value;

        public static bool ContainsAny(this ActorMask mask, ActorMask value)
            => (mask & value) != 0;
    }
}
