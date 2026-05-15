using System;
using BattleBase.DI;
using BattleBase.Gameplay.CameraNavigation.InputReader;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay
{
    public class BuildingSiteMediator : MonoBehaviour, IInjectable
    {
        private IClickDetector _clickDetector;
        private IBuildingSiteSelector _selector;

        [Inject]
        public void Construct(IClickDetector clickDetector, IBuildingSiteSelector selector)
        {
            _clickDetector = clickDetector ?? throw new ArgumentNullException(nameof(clickDetector));
            _selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        private void OnEnable() =>
            _clickDetector.Clicked += OnClick;

        private void OnDisable() =>
            _clickDetector.Clicked -= OnClick;

        private void OnClick(Collider collider)
        {
            if (collider == null)
                return;

            if (collider.TryGetComponent(out IBuildingSite buildingSite))
                _selector.TrySelect(buildingSite);
            else
                _selector.Unselect();
        }
    }
}