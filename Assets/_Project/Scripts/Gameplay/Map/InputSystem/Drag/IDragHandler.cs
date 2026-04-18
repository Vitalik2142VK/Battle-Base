using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public interface IDragHandler
    {
        Vector3? Update(float deltaTime);
    }
}