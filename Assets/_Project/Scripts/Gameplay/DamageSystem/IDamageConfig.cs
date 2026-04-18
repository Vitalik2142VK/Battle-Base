namespace BattleBase.Gameplay.Units
{
    public interface IDamageConfig
    {
        public DamageMask DamageMask { get; }

        public float Damage { get; }
    }
}
