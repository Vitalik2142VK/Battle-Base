using System;

namespace BattleBase.Gameplay.Map
{
    public interface ICameraOrientationAdapter
    {
        public event Action Changed;

        public float CurrentOrtoSize { get; }

        public float MinimumOrtoSize { get; }

        public float MaximumOrtoSize { get; }
    }
}