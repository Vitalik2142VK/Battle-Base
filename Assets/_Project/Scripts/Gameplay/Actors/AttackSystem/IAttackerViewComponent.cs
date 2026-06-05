namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IAttackerViewComponent : IActorViewComponent
    {
        public void Init(IAttackEvents weaponEvents);
    }
}