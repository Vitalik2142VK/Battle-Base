using BattleBase.Gameplay.Units;

namespace BattleBase.Gameplay.Weapons
{
    public interface IWeapon
    {
        public void ShootUnit(IUnit unit);
    }
}