namespace BattleBase.Gameplay.Actors.Building
{
    public interface IBuildingSite
    {
        public bool TrySelect();

        public void Unselect();

        public void Show();

        public void Hide();

        public void SetActiveState();

        public void SetInactiveState();
    }
}