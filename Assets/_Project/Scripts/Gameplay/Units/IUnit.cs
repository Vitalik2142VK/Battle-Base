using BattleBase.Gameplay.DamageSystem;
using BattleBase.Gameplay.Weapons;

namespace BattleBase.Gameplay.Units
{
    public interface IUnit : ITargetPoint, IDamageble 
    {
        public SideUnit SideUnit { get; }
    }
}
