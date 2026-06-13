namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public interface IDamage
    {
        public ActorMask ActorMask { get; }

        public DamageMask DamageMask { get; }

        public float Value { get; }
    }
}