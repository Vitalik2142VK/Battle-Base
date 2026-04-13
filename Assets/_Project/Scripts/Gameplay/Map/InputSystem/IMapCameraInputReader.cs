using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public interface IMapCameraInputReader
    {
        public Vector2? DragDelta { get; }

        public float? ZoomDelta { get; }
    }
}