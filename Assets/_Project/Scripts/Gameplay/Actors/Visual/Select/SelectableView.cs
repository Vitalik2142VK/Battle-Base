using System;
using System.Collections;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Select
{
    public abstract class SelectableView : MonoBehaviour
    {
        [SerializeField] private Selectable _selectable;

        private void OnEnable()
        {
            _selectable.StateChanged += UpdateState;

            StartCoroutine(LateUpdateState());
        }

        private void OnDisable()
        {
            _selectable.StateChanged -= UpdateState;
        }

        private void UpdateState()
        {
            switch (_selectable.State)
            {
                case SelectableState.Inactive:
                    HandleInactiveState();
                    break;

                case SelectableState.Active:
                    HandleActiveState();
                    break;

                case SelectableState.Selected:
                    HandleSelectedState();
                    break;

                default:
                    throw new InvalidOperationException("The specified type is not registered");
            }
        }

        private IEnumerator LateUpdateState()
        {
            yield return null;

            UpdateState();
        }

        protected abstract void HandleInactiveState();

        protected abstract void HandleActiveState();

        protected abstract void HandleSelectedState();
    }
}