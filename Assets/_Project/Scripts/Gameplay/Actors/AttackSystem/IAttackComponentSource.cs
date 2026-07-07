using BattleBase.Gameplay.Actors.AttackSystem.Weapons;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IAttackComponentSource : IComponentSource
    {
        public IWeaponConfig Config { get; }
    }
}