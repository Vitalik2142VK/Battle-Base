namespace BattleBase.Gameplay.Actors.Building
{
    public interface IBuildingSite : IActorViewComponent
    {
        public int NumberLine { get; }

        public void Show();

        public void Hide();
    }
}