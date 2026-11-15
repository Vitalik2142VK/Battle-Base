using BattleBase.Gameplay.Actors.Colored;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Select
{
    public class ColorSelectableView : SelectableView
    {
        [SerializeField] private MaterialColorChanger _colorChanger;
        [SerializeField] private Color _inactiveColor = Color.white;
        [SerializeField] private Color _activeColor = Color.white;
        [SerializeField] private Color _selectColor = Color.yellow;
        
        protected override void HandleInactiveState() => 
            _colorChanger.Change(_inactiveColor);

        protected override void HandleActiveState() => 
            _colorChanger.Change(_activeColor);

        protected override void HandleSelectedState() => 
            _colorChanger.Change(_selectColor);
    }
}