namespace BattleBase.Gameplay.Actors.Building
{
    public interface IBuildingSiteSelector
    {
        public bool TrySelect(IBuildingSite site);

        public void Unselect();
    }
}