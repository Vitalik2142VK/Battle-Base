using BattleBase.Gameplay.DamageSystem;
using BattleBase.Gameplay.Weapons;

namespace BattleBase.Gameplay.Actors
{
    public interface IUnit : ITargetPoint, IDamageble 
    {
        public SideUnit SideUnit { get; }

        public float ConstructionTime { get; }

        public void SetSide(SideUnit side);
    }
}
