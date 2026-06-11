using BattleBase.Gameplay.Actors.AttackSystem.Ammo;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IProjectileSpawner
    {
        public IProjectile Spawn(string missileId);
    }
}