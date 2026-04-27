namespace BattleBase.Gameplay.Units
{
    public interface IDamageConfig
    {
        public float Damage { get; }

        public float RateShooting { get; }

        public float SpeedReload { get; }

        public int NumberShells { get; }
    }
}
