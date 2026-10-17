using UnityEngine;

namespace BattleBase.Gameplay.CameraNavigation.InputReader
{
    public interface IUIPointerChecker
    {
        public bool IsPointerOverUI(Vector2 screenPosition);
    }
}