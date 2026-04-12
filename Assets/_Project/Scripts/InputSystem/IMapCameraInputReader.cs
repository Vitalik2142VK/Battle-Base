using UnityEngine;

namespace BattleBase.InputSystem
{
    public interface IMapCameraInputReader
    {
        public Vector2? DragDelta { get; }
    }
}