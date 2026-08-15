namespace BattleBase.Gameplay.Actors.Building
{
    public interface IBuildingSite : IActorViewComponent
    {
        public SiteType Type { get; }

        public int Id { get; }

        public int NumberLine { get; }

        public void Select();

        public void Unselect();
    }
}