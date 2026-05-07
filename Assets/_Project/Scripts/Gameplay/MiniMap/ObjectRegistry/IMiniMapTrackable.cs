using UnityEngine;

namespace BattleBase.Gameplay.MiniMap
{
    public interface IMiniMapTrackable 
    { 
        public Color Color { get; }

        public Vector2 WorldSize { get; }

        public Vector3 WorldPosition { get; }

        public float RotationY { get; }
    }
}