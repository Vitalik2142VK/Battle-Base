namespace BattleBase.Gameplay.Actors.Visual.Select
{
    public interface ISelector
    {
        public bool TrySelect(ISelectable selectable);

        public void Unselect();
    }
}