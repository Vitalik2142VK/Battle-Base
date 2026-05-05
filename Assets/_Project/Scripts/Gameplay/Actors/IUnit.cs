using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.Weapons;

namespace BattleBase.Gameplay.Actors
{
    public interface IUnit : ITargetPoint, IDamageble 
    {
        public TeamType TeamType { get; }

        public float ConstructionTime { get; }

        public void SetSide(TeamType side);
    }
}
