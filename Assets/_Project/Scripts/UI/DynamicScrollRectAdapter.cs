using BattleBase.Gameplay.CameraNavigation;
using UnityEngine;
using VContainer;

namespace BattleBase.UI
{
    public class DynamicScrollRectAdapter : MonoBehaviour
    {
        [SerializeField] private DynamicScrollRect _dynamicScrollRect;
        [SerializeField][Min(1)] private int _maxColumsInVerticalOrientation = 3;
        [SerializeField][Min(1)] private int _maxColumsInHorisontalOrientation = 6;

        private IScreenOrientationTracker _screenOrientationTracker;

        [Inject]
        public void Contruct(IScreenOrientationTracker screenOrientationTracker)
        {
            _screenOrientationTracker = screenOrientationTracker ?? throw new System.ArgumentNullException(nameof(screenOrientationTracker));
        }

        private void OnEnable()
        {
            _screenOrientationTracker.OrientationChanged += OnUgradeScroll;

            OnUgradeScroll();
        }

        private void OnDisable()
        {
            _screenOrientationTracker.OrientationChanged -= OnUgradeScroll;
        }

        private void OnUgradeScroll()
        {
            if (_screenOrientationTracker.ScreenOrientation == ScreenOrientationType.Portrait)
                _dynamicScrollRect.SetMaxVisibleColumns(_maxColumsInVerticalOrientation);
            else
                _dynamicScrollRect.SetMaxVisibleColumns(_maxColumsInHorisontalOrientation);

            _dynamicScrollRect.Refresh();
        }
    }
}