namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public interface IDamageConfig
    {
        public ActorMask ActorMask { get; }
        
        public DamageMask DamageMask { get; }

        public float Damage { get; }
    }
}
