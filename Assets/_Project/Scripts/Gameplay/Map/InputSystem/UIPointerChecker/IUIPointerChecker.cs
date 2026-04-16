using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public interface IUIPointerChecker
    {
        public void AddCanvas(Canvas canvas);

        public bool IsPointerOverUI(Vector2 screenPosition);
    }
}