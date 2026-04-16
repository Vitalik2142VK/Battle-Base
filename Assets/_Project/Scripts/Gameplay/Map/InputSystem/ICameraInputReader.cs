using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public interface ICameraInputReader
    {
        public Vector3? WorldDragDelta { get; }

        public float? ZoomDelta { get; }
    }
}