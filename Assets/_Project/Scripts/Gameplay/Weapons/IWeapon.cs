using BattleBase.Gameplay.Actors;

namespace BattleBase.Gameplay.Weapons
{
    public interface IWeapon
    {
        public void ShootUnit(IUnit unit);
    }
}