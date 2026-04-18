using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public interface IKeyboardDragHandler
    {
        public Vector3? Update(float deltaTime);
    }
}