using BattleBase.Gameplay.Actors.AttackSystem.Missiles;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IMissileSpawner
    {
        public IMissile Spawn(string missileId);
    }
}