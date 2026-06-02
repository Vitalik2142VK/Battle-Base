namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IAttackComponentSource : IComponentSource
    {
        public IWeaponConfig Config { get; }
    }
}