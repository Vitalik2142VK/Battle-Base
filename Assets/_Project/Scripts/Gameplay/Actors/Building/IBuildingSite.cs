namespace BattleBase.Gameplay.Actors.Building
{
    public interface IBuildingSite : IActorViewComponent
    {
        public SiteType Type { get; }

        public int NumberLine { get; }

        public void Show();

        public void Hide();
    }
}