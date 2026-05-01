using System;

namespace BattleBase.Gameplay.CameraNavigation
{
    public interface IScreenOrientationTracker
    {
        public event Action OrientationChanged;

        public bool IsPortrait { get; }

        public int Width { get; }

        public int Height { get; }
    }
}