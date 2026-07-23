namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public interface IProjectileConfig
    {
        public string ProjectileId { get; }

        public float Speed { get; }
    }
}