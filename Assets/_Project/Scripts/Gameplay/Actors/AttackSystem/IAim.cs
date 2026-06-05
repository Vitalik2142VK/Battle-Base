namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IAim : IActorViewComponent
    {
        public void Init(IAttackerPresenter presenter, IAttackEvents attackEvents);
    }
}