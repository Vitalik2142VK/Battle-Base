using System;
using BattleBase.DI;
using BattleBase.Gameplay.CameraNavigation;
using UnityEngine;
using VContainer;

namespace BattleBase.ShopSystem
{
    public class ScreenOrientationMarkupSwitcher : MonoBehaviour, IInjectable
    {
        [SerializeField] private GameObject _vertical;
        [SerializeField] private GameObject _horizontal;

        private IScreenOrientationTracker _tracker;

        [Inject]
        public void Construct(IScreenOrientationTracker tracker) =>
            _tracker = tracker ?? throw new ArgumentNullException(nameof(tracker));

        private void OnEnable()
        {
            _tracker.OrientationChanged += Switch;
            Switch();
        }

        private void OnDisable() =>
            _tracker.OrientationChanged -= Switch;

        private void Switch()
        {
            if (_tracker.ScreenOrientation == ScreenOrientationType.Landscape)
            {
                _vertical.SetActive(false);
                _horizontal.SetActive(true);
            }
            else
            {
                _horizontal.SetActive(false);
                _vertical.SetActive(true);
            }
        }
    }
}