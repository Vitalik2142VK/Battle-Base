using BattleBase.Gameplay.Actors.Colored;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Select
{
    public class ColorSelectableView : SelectableView
    {
        [SerializeField] private MaterialColorChanger _colorChanger;
        [SerializeField] private Color _selectColor = Color.yellow;

        private Color _oldColor;

        private void Start()
        {
            _oldColor = _colorChanger.CurrentColor;
        }

        protected override void HandleInactiveState() => 
            _colorChanger.Change(_oldColor);

        protected override void HandleActiveState() => 
            _colorChanger.Change(_oldColor);

        protected override void HandleSelectedState()
        {
            _oldColor = _colorChanger.CurrentColor;
            _colorChanger.Change(_selectColor);
        }
    }
}