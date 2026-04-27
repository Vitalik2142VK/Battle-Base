using System;

namespace BattleBase.Gameplay.CameraNavigation
{
    public interface ICameraOrientationAdapter
    {
        public event Action Changed;

        public float CurrentOrtoSize { get; }

        public float MinimumOrtoSize { get; }

        public float MaximumOrtoSize { get; }
    }
}