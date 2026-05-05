using System;

namespace BattleBase.Gameplay.CameraNavigation
{
    public class ScreenOrientationTracker : IScreenOrientationTracker, IDisposable
    {
        private readonly IScreenSizeTracker _screenSizeTracker;

        public ScreenOrientationTracker(IScreenSizeTracker screenSizeTracker)
        {
            _screenSizeTracker = screenSizeTracker ?? throw new ArgumentNullException(nameof(screenSizeTracker));

            IsPortrait = IsCurrentOrientationPortrait();
            _screenSizeTracker.SizeChanged += OnSizeChanged;
        }

        public event Action OrientationChanged;

        public bool IsPortrait { get; private set; }

        public int Width => _screenSizeTracker.Width;

        public int Height => _screenSizeTracker.Height;

        public void Dispose() =>
            _screenSizeTracker.SizeChanged -= OnSizeChanged;

        private bool IsCurrentOrientationPortrait() =>
            _screenSizeTracker.Height > _screenSizeTracker.Width;

        private void OnSizeChanged()
        {
            bool currentIsPortrait = IsCurrentOrientationPortrait();

            if (currentIsPortrait == IsPortrait)
                return;

            IsPortrait = currentIsPortrait;
            OrientationChanged?.Invoke();
        }
    }
}