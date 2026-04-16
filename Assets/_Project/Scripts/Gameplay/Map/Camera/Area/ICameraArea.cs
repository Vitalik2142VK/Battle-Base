using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public interface ICameraArea
    {
        public float Resistance { get; }

        public float Overshoot { get; }

        public float PlaneY { get; }

        public Bounds ColliderBounds { get; }

        public Bounds OvershootBounds { get; }
    }
}