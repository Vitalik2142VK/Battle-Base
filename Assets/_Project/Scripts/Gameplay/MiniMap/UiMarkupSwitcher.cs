using System;
using BattleBase.DI;
using BattleBase.Gameplay.CameraNavigation;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.MiniMap
{
    public class UiMarkupSwitcher : MonoBehaviour, IInjectable
    {
        [SerializeField] private GameObject _verticalCanvas;
        [SerializeField] private GameObject _horizontalCanvas;
        [SerializeField] private float _verticalCameraRotationY;
        [SerializeField] private float _horizontalCameraRotationY;

        private Transform _cameraRig;
        private IScreenOrientationTracker _orientationTracker;

        [Inject]
        public void Construct(CameraRig cameraRig, IScreenOrientationTracker orientationTracker)
        {
            if (cameraRig == null)
                throw new ArgumentNullException(nameof(cameraRig));

            _cameraRig = cameraRig.transform;

            _orientationTracker = orientationTracker ?? throw new ArgumentNullException(nameof(orientationTracker));
        }

        private void OnEnable()
        {
            _orientationTracker.OrientationChanged += OnOrientationChanged;
            OnOrientationChanged();
        }

        private void OnDisable() =>
            _orientationTracker.OrientationChanged -= OnOrientationChanged;

        private void OnOrientationChanged()
        {
            bool isPortrait = _orientationTracker.IsPortrait;
            _verticalCanvas.SetActive(isPortrait);
            _horizontalCanvas.SetActive(isPortrait == false);
            Vector3 angles = _cameraRig.transform.eulerAngles;
            angles.y = isPortrait ? _verticalCameraRotationY : _horizontalCameraRotationY;
            _cameraRig.transform.eulerAngles = angles;
        }
    }
}