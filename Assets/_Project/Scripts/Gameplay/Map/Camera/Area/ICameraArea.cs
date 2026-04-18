using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public interface ICameraArea
    {
        public float Resistance { get; }

        public float ResistanceFadeDistance { get; }

        public float GroundPlaneY { get; }

        public Bounds ColliderBounds { get; }

        public Bounds OvershootBounds { get; }
    }
}