namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public interface IDamageConfig
    {
        public ITargetingProfile TargetingProfile { get; }
        
        public DamageMask DamageMask { get; }

        public float Damage { get; }
    }
}
