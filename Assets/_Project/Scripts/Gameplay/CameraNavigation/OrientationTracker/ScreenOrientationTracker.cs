using System;

namespace BattleBase.Gameplay.CameraNavigation
{
    public class ScreenOrientationTracker : IScreenOrientationTracker, IDisposable
    {
        private const float PortraitAspectThreshold = 1f;

        private readonly IScreenSizeTracker _screenSizeTracker;

        public ScreenOrientationTracker(IScreenSizeTracker screenSizeTracker)
        {
            _screenSizeTracker = screenSizeTracker ?? throw new ArgumentNullException(nameof(screenSizeTracker));

            IsPortrait = GetCurrentIsPortrait();
            _screenSizeTracker.SizeChanged += OnSizeChanged;
        }

        public event Action OrientationChanged;

        public bool IsPortrait { get; private set; }

        public int Width => _screenSizeTracker.Width;

        public int Height => _screenSizeTracker.Height;

        public void Dispose() =>
            _screenSizeTracker.SizeChanged -= OnSizeChanged;

        private bool GetCurrentIsPortrait()
        {
            float aspect = (float)_screenSizeTracker.Width / _screenSizeTracker.Height;
            
            return aspect < PortraitAspectThreshold;
        }

        private void OnSizeChanged()
        {
            bool currentIsPortrait = GetCurrentIsPortrait();

            if (currentIsPortrait == IsPortrait)
                return;

            IsPortrait = currentIsPortrait;
            OrientationChanged?.Invoke();
        }
    }
}