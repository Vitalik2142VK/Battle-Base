using System;
using UnityEngine;

namespace BattleBase.Gameplay
{
    public class BuildingSite : Entity, IBuildingSite
    {
        [SerializeField] private Color _color;
        [SerializeField] private bool _isPlayer;
        [SerializeField] private BuildingSiteState _state;

        public event Action StateChanged;

        public bool IsPlayer => _isPlayer;

        public BuildingSiteState State => _state;

        private void Awake() =>
            SetColor(_color);

        public bool TrySelect()
        {
            if (_isPlayer && _state == BuildingSiteState.Active)
            {
                _state = BuildingSiteState.Selected;
                StateChanged?.Invoke();

                return true;
            }

            return false;
        }

        public void Unselect()
        {
            if(_isPlayer && _state == BuildingSiteState.Selected)
            {
                _state = BuildingSiteState.Active;
                StateChanged?.Invoke();
            }
        }
    }
}