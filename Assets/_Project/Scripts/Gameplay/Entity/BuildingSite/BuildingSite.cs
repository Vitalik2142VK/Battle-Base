using System;
using UnityEngine;

namespace BattleBase.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class BuildingSite : MonoBehaviour, IBuildingSite
    {
        [SerializeField] private BuildingSiteState _state;

        private Collider _collider;

        public event Action StateChanged;

        public BuildingSiteState State => _state;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public bool TrySelect()
        {
            if (_state == BuildingSiteState.Active)
            {
                _state = BuildingSiteState.Selected;
                StateChanged?.Invoke();

                return true;
            }

            return false;
        }

        public void Unselect()
        {
            if (_state == BuildingSiteState.Selected)
            {
                _state = BuildingSiteState.Active;
                StateChanged?.Invoke();
            }
        }

        public void SetActiveState()
        {
            _collider.enabled = true;
            _state = BuildingSiteState.Active;
        }

        public void SetInactiveState()
        {
            _collider.enabled = false;
            _state = BuildingSiteState.Inactive;
            StateChanged?.Invoke();
        }
    }
}