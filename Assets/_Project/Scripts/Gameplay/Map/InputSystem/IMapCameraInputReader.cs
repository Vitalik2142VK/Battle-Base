using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public interface IMapCameraInputReader
    {
        Vector3? WorldDragDelta { get; }

        public float? ZoomDelta { get; }
    }
}