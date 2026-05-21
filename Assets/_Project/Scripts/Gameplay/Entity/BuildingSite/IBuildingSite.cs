namespace BattleBase.Gameplay
{
    public interface IBuildingSite
    {
        public bool TrySelect();

        public void Unselect();

        public void SetActiveState();

        public void SetInactiveState();
    }
}