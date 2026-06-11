namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public interface IDamageConfig
    {
        public DamageMask DamageMask { get; }

        public float Damage { get; }
    }
}
