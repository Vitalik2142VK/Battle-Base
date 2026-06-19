using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Select
{
    public class Selectable : MonoBehaviour, ISelectable
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private SelectableState _state;

        public event Action StateChanged;

        public SelectableState State => _state;

        public bool TrySelect()
        {
            if (_state == SelectableState.Active)
            {
                _state = SelectableState.Selected;
                StateChanged?.Invoke();

                return true;
            }

            return false;
        }

        public void Unselect()
        {
            if (_state == SelectableState.Selected)
            {
                _state = SelectableState.Active;
                StateChanged?.Invoke();
            }
        }

        public void SetInactiveState()
        {
            _collider.enabled = false;
            _state = SelectableState.Inactive;
            StateChanged?.Invoke();
        }
    }
}