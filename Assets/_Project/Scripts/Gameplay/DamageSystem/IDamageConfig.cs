namespace BattleBase.Gameplay.Actors
{
    public interface IDamageConfig
    {
        public DamageMask DamageMask { get; }

        public float Damage { get; }
    }
}
