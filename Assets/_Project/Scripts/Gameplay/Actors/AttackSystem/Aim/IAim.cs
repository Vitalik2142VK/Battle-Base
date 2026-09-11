namespace BattleBase.Gameplay.Actors.AttackSystem.Aim
{
    public interface IAim : IActorViewComponent
    {
        public void Init(IAttackerPresenter presenter, IAttackNotifier attackNotifier);
    }
}