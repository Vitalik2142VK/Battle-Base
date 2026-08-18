namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface ITargetFinder : IActorViewComponent
    {
        public void Init(IAttackerPresenter presenter, ITargetFinderConfig targetConfig, ITeamable teamable);
    }
}
