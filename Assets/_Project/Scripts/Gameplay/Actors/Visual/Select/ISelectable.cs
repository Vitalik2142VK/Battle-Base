namespace BattleBase.Gameplay.Actors.Visual.Select
{
    public interface ISelectable
    {
        public bool TrySelect();

        public void Unselect();

        public void SetInactiveState();
    }
}