namespace BattleBase.Gameplay.Actors.Visual.Select
{
    public class Selector : ISelector
    {
        private ISelectable _selected;

        public bool TrySelect(ISelectable selectable)
        {
            if (selectable == null)
                return false;

            if (_selected == selectable)
                return true;

            if (selectable.TrySelect())
            {
                Unselect();
                _selected = selectable;

                return true;
            }

            return false;
        }

        public void Unselect()
        {
            if (_selected != null)
            {
                _selected.Unselect();
                _selected = null;
            }
        }
    }
}